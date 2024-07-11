using UniVerServer.Enrollments.Enums;

namespace UniVerServer.Enrollments.DTO;

public class UpdateStatusDto
{
    public EnrollmentStatus Status { get; set; }
    public Guid CourseId { get; set; }
}