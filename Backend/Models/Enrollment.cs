using System;

namespace StudentCourseRegistrationSystem.Models
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public DateTime EnrolledDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}
