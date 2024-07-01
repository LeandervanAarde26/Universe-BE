namespace UniVerServer.Subjects.DTO;

public class SubjectInCourseDto
{
    public Guid Id { get; set; }
    public string SubjectName { get; set; }
    public int SubjectCredits { get; set; } = 20;
    public int SubjectYear { get; set; }
    public int ClassRuntime { get; set; } = 120;
    public string SubjectIdentifier { get; set; }
}