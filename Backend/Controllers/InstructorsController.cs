using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseRegistrationSystem.Data;
using StudentCourseRegistrationSystem.DTOs;
using StudentCourseRegistrationSystem.Models;

namespace StudentCourseRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InstructorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Instructors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetInstructors()
        {
            var instructors = await _context.Instructors
                .Select(i => new InstructorDto
                {
                    Id = i.Id,
                    Name = i.Name,
                    Department = i.Department
                })
                .ToListAsync();

            return Ok(instructors);
        }

        // POST: api/Instructors
        [HttpPost]
        public async Task<ActionResult<InstructorDto>> CreateInstructor(InstructorDto instructorDto)
        {
            var instructor = new Instructor
            {
                Name = instructorDto.Name,
                Department = instructorDto.Department
            };

            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            instructorDto.Id = instructor.Id;

            return CreatedAtAction(nameof(GetInstructors), new { id = instructor.Id }, instructorDto);
        }

        // PUT: api/Instructors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstructor(int id, InstructorUpdateDto instructorDto)
        {
            var instructor = await _context.Instructors.FindAsync(id);

            if (instructor == null)
            {
                return NotFound();
            }

            instructor.Name = instructorDto.Name;
            instructor.Department = instructorDto.Department;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(id))
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

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.Id == id);
        }
    }
}
