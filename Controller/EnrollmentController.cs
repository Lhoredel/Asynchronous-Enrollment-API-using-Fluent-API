using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asynchronous_Enrollment_API_using_Fluent_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController: ControllerBase
    {
        private readonly Data.ApplicationDbContext _dbContext;
        public EnrollmentController(Data.ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> EnrollStudent(Guid studentId, Guid courseId)
        {
            var student = await _dbContext.Students.FindAsync(studentId);
            var course = await _dbContext.Courses.FindAsync(courseId);
            if (student == null || course == null)
            {
                return NotFound("Student or Course not found.");
            }
            var enrollment = new Models.Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrolledDate = DateTime.UtcNow
            };
            _dbContext.Enrollments.Add(enrollment);
            await _dbContext.SaveChangesAsync();
            return Ok("Student enrolled successfully.");
        }
        [HttpGet]
        [Route("unenroll")]
        public async Task<IActionResult> UnenrollStudent(Guid studentId, Guid courseId)
        {
            var enrollment = await _dbContext.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
            {
                return NotFound("Enrollment not found.");
            }
            _dbContext.Enrollments.Remove(enrollment);
            await _dbContext.SaveChangesAsync();
            return Ok("Student unenrolled successfully.");
        }
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateEnrollment(Guid studentId, Guid courseId, DateTime newEnrolledDate)
        {
            var enrollment = await _dbContext.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
            {
                return NotFound("Enrollment not found.");
            }
            enrollment.EnrolledDate = newEnrolledDate;
            _dbContext.Enrollments.Update(enrollment);
            await _dbContext.SaveChangesAsync();
            return Ok("Enrollment updated successfully.");
        }
        [HttpPost]
        [Route("get")]
        public async Task<IActionResult> GetEnrollment(Guid studentId, Guid courseId)
        {
            var enrollment = await _dbContext.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
            {
                return NotFound("Enrollment not found.");
            }
            return Ok(enrollment);
        }
        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await _dbContext.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToListAsync();
            return Ok(enrollments);
        }
        [HttpPost]
        [Route("delete")]
        public
            async Task<IActionResult> DeleteEnrollment(Guid studentId, Guid courseId)
        {
            var enrollment = await _dbContext.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            if (enrollment == null)
            {
                return NotFound("Enrollment not found.");
            }
            _dbContext.Enrollments.Remove(enrollment);
            await _dbContext.SaveChangesAsync();
            return Ok("Enrollment deleted successfully.");
        }
        [HttpPost]
        [Route("deleteall")]
        public async Task<IActionResult> DeleteAllEnrollments()
        {
            var enrollments = await _dbContext.Enrollments.ToListAsync();
            _dbContext.Enrollments.RemoveRange(enrollments);
            await _dbContext.SaveChangesAsync();
            return Ok("All enrollments deleted successfully.");
        }

    }
    }
