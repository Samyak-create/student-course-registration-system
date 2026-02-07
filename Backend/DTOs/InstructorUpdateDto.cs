using System.ComponentModel.DataAnnotations;

namespace StudentCourseRegistrationSystem.DTOs
{
    public class InstructorUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Department { get; set; } = string.Empty;
    }
}
