using UniVerServer.Enrollments.Enums;

namespace UniVerServer.Enrollments.DTO;

public class CreateEnrollmentRequestDto
{
    public string  CourseId { get; set; }
    public string StudentId { get; set; }
    public EnrollmentStatus Status { get; set; } = EnrollmentStatus.INCOMPLETE;
    public decimal Grade { get; set; }
    public GradeType GradeType { get; set; } = GradeType.INCOMPLETE;
}