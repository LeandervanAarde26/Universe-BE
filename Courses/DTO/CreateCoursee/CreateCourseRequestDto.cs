namespace UniVerServer.Courses.DTO;

public class CreateCourseRequestDto
{
    public string SubjectId { get; set; }
    public string StartDate { get; set; }
    public bool Active { get; set; } = false;
    public bool AcceptingStudents { get; set; } = false;
    
    public CreateCourseDto ConvertCourseDto() => 
    new()
    {
        AcceptingStudents = AcceptingStudents,
        Active = Active,
        StartDate = DateTime.Parse(StartDate).ToUniversalTime(),
        SubjectId = Guid.Parse(SubjectId)
    };
}