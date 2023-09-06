namespace UniVerServer.Models.DTO
{
    public class LecturerWithCourses
    {

        public int lecturer_id { get; set; }
        public string lecturer_name { get; set; }
        public string email { get; set; }
        public string lecturer_phoneNumber { get; set; }
        public string role { get; set; }
        public int lecturer_rate { get; set; }
        public List<SubjectEnrollments> enrollments { get; set; }
    }
}
