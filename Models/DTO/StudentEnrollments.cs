namespace UniVerServer.Models.DTO
{
    public class StudentEnrollments
    {
        public Users.Models.Users Student { get; set; }
        public CourseEnrollments Course { get; set; }
    }
}
