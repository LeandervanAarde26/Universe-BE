using UniVerServer.Courses.DTO;
using UniVerServer.Subjects.DTO;
using UniVerServer.Users.DTO;

namespace UniVerServer.Enrollments.DTO;

public class GetEnrollmentsDto
{
    // subject information
    public SubjectInCourseDto Subject { get; set; }
    // course information
    public GetCourseEnrollmentsDto Course { get; set; }
    // student/s information
    public ICollection<StudentEnrollmentDto> Students { get; set; }
}