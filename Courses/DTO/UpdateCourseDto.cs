namespace UniVerServer.Courses.DTO;

public class UpdateCourseDto
{
    public DateTime StartDate { get; set; }
    public bool Active { get; set; } = false;
    public bool AcceptingStudents { get; set; } = false;
}