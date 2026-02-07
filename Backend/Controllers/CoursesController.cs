using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseRegistrationSystem.Data;
using StudentCourseRegistrationSystem.DTOs;
using StudentCourseRegistrationSystem.Models;

namespace StudentCourseRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetCourses()
        {
            var courses = await _context.Courses
                .Include(c => c.Instructor)
                .Where(c => c.IsActive) // Soft delete check is already in query filter, but good for explicit
                .Select(c => new CourseReadDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Credits = c.Credits,
                    InstructorName = c.Instructor != null ? c.Instructor.Name : "Unassigned"
                })
                .ToListAsync();

            return Ok(courses);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<CourseReadDto>> CreateCourse(CourseCreateDto courseDto)
        {
            var course = new Course
            {
                Title = courseDto.Title,
                Credits = courseDto.Credits,
                InstructorId = courseDto.InstructorId
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            
             // Load instructor for response
            await _context.Entry(course).Reference(c => c.Instructor).LoadAsync();

            var readDto = new CourseReadDto
            {
                Id = course.Id,
                Title = course.Title,
                Credits = course.Credits,
                InstructorName = course.Instructor != null ? course.Instructor.Name : "Unassigned"
            };

            return CreatedAtAction(nameof(GetCourses), new { id = course.Id }, readDto);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            // Soft delete
            course.IsActive = false;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Courses/5/students
        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetEnrolledStudents(int id)
        {
             var course = await _context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.Student)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var students = course.Enrollments
                 .Where(e => e.Student != null)
                 .Select(e => new StudentReadDto
                 {
                     Id = e.Student!.Id,
                     Name = e.Student.Name,
                     Email = e.Student.Email,
                     CreatedAt = e.Student.CreatedAt
                 }).ToList();

            return Ok(students);
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseUpdateDto courseDto)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }
            
            // Validate Instructor Exists
            var instructor = await _context.Instructors.FindAsync(courseDto.InstructorId);
            if (instructor == null)
            {
                 return BadRequest("Invalid Instructor ID");
            }

            course.Title = courseDto.Title;
            course.Credits = courseDto.Credits;
            course.InstructorId = courseDto.InstructorId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
