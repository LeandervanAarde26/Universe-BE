using UniVerServer.Abstractions;
using UniVerServer.Courses.Models;
using UniVerServer.Enrollments.Enums;

namespace UniVerServer.Enrollments.Models;

public class Enrollment : BaseModel
{
    public Users.Models.Users Student { get; set; }
    public Guid StudentId { get; set; }
    public Course Course { get; set; }
    public Guid  CourseId { get; set; }
    public EnrollmentStatus Status { get; set; }
    public decimal Grade { get; set; }
    public GradeType GradeType { get; set; }
    public DateTime Modified { get; set; }
}
