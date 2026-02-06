using System.ComponentModel.DataAnnotations;

namespace StudentCourseRegistrationSystem.DTOs
{
    public class CourseCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Range(1, 10)]
        public int Credits { get; set; }

        public int InstructorId { get; set; }
    }
}
