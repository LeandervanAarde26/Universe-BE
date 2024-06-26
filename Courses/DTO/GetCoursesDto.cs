using UniVerServer.Subjects.DTO;
using UniVerServer.Users.DTO;

namespace UniVerServer.Courses.DTO;

public class GetCoursesDto
{
    public Guid Id { get; set; }
    // Course Information
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Active { get; set; } = false;
    public bool AcceptingStudents { get; set; } = false;
    
    public SubjectInCourseDto Subject { get; set; }
    // Lecturer information
    public LecturerInformation Lecturer { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}