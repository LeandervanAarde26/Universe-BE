using UniVerServer.Enrollments.Enums;

namespace UniVerServer.Enrollments.DTO;

public class UpdateEnrollmentStatusRequestDto
{
    public EnrollmentStatus Status { get; set; }
    public string CourseId { get; set; }
}