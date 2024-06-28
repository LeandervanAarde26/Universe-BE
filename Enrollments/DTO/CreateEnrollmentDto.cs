using UniVerServer.Enrollments.Enums;

namespace UniVerServer.Enrollments.DTO;

public class CreateEnrollmentDto
{
    public Guid  CourseId { get; set; }
    public Guid StudentId { get; set; }
    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.INCOMPLETE;
    public decimal Grade { get; set; }
    public GradeType GradeType { get; set; } = GradeType.INCOMPLETE;
}