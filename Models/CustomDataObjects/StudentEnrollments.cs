namespace UniVerServer.Models.CustomDataObjects
{
    public class StudentEnrollments
    {
        public People Student { get; set; }
        public CourseEnrollments Course { get; set; }
    }
}
