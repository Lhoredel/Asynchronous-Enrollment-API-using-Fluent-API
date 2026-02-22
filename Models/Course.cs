namespace Asynchronous_Enrollment_API_using_Fluent_API.Models
{
    public class Course
    {
        public Guid  Id { get; set; }
        public string CourseName { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
