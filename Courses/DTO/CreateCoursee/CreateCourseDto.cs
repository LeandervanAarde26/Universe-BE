namespace UniVerServer.Courses.DTO;

public class CreateCourseDto
{
    public Guid SubjectId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; private set; }
    public bool Active { get; set; } = false;
    public bool AcceptingStudents { get; set; } = false;
    
    public void SetEndDate(DateTime endDate)
    {
        EndDate = endDate;
    }
    
}