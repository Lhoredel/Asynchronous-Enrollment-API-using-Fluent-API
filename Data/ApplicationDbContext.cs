using Microsoft.EntityFrameworkCore;
using System;

namespace Asynchronous_Enrollment_API_using_Fluent_API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextoptions) : base(dbContextoptions)
        {
        }

        public DbSet<Models.Student> Students { get; set; }
        public DbSet<Models.Course> Courses { get; set; }   
        public DbSet<Models.Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the many-to-many relationship between Student and Course through Enrollment
            modelBuilder.Entity<Models.Enrollment>()
                .HasKey(e => new { e.StudentId, e.CourseId });
            modelBuilder.Entity<Models.Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);
            modelBuilder.Entity<Models.Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);
        }
        
    }
}
