using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseRegistrationSystem.Data;
using StudentCourseRegistrationSystem.DTOs;
using StudentCourseRegistrationSystem.Models;

namespace StudentCourseRegistrationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Enrollments
        [HttpPost]
        public async Task<ActionResult> EnrollStudent(EnrollmentCreateDto enrollmentDto)
        {
            // Validate Student and Course exist
            var student = await _context.Students.FindAsync(enrollmentDto.StudentId);
            if (student == null) return NotFound("Student not found");

            var course = await _context.Courses.FindAsync(enrollmentDto.CourseId);
            if (course == null) return NotFound("Course not found");

            // Prevent duplicate enrollment
            bool exists = await _context.Enrollments.AnyAsync(e => 
                e.StudentId == enrollmentDto.StudentId && e.CourseId == enrollmentDto.CourseId);
            
            if (exists)
            {
                return BadRequest("Student is already enrolled in this course.");
            }

            var enrollment = new Enrollment
            {
                StudentId = enrollmentDto.StudentId,
                CourseId = enrollmentDto.CourseId
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return Ok("Enrolled successfully");
        }
    }
}
