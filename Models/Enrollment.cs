namespace Asynchronous_Enrollment_API_using_Fluent_API.Models
{
    public class Enrollment
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime EnrolledDate { get; set; }
    }
}
