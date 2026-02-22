 namespace Asynchronous_Enrollment_API_using_Fluent_API.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
