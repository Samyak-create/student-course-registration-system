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
    }
}
