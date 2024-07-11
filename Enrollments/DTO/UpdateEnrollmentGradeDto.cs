namespace UniVerServer.Enrollments.DTO;

public class UpdateEnrollmentGradeDto
{
    public Guid StudentId { get; set; }

    public decimal grade { get; set; }
}