namespace UniVerServer.Courses.DTO;

public class GetCourseEnrollmentsDto
{
    // course information
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Active { get; set; } = false;
}