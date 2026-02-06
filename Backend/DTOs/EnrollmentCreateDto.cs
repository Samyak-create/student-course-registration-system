using System.ComponentModel.DataAnnotations;

namespace StudentCourseRegistrationSystem.DTOs
{
    public class EnrollmentCreateDto
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
