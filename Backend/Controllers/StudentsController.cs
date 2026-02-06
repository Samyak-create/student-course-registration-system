using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseRegistrationSystem.Data;
using StudentCourseRegistrationSystem.DTOs;
using StudentCourseRegistrationSystem.Models;

namespace StudentCourseRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentReadDto>>> GetStudents()
        {
            var students = await _context.Students
                .Select(s => new StudentReadDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    CreatedAt = s.CreatedAt
                })
                .ToListAsync();

            return Ok(students);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentReadDto>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return new StudentReadDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                CreatedAt = student.CreatedAt
            };
        }

        // GET: api/Students/5/courses
        [HttpGet("{id}/courses")]
        public async Task<ActionResult<IEnumerable<CourseReadDto>>> GetStudentCourses(int id)
        {
            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ThenInclude(c => c.Instructor)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            var courses = student.Enrollments.Select(e => new CourseReadDto
            {
                Id = e.Course!.Id,
                Title = e.Course.Title,
                Credits = e.Course.Credits,
                InstructorName = e.Course.Instructor != null ? e.Course.Instructor.Name : "Unknown"
            }).ToList();

            return Ok(courses);
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<StudentReadDto>> CreateStudent(StudentCreateDto studentDto)
        {
            var student = new Student
            {
                Name = studentDto.Name,
                Email = studentDto.Email
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var readDto = new StudentReadDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                CreatedAt = student.CreatedAt
            };

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, readDto);
        }
    }
}
