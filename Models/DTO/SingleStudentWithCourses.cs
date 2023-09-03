namespace UniVerServer.Models.DTO
{

    public class SubjectEnrollments
    {
        public int subject_id { get; set; }
        public string subject_name { get; set;}
        public string subject_code { get; set;}
        public string subject_color { get; set;}
    }
    public class SingleStudentWithCourses
    {
       public int student_id {get; set;}
       public string person_system_identifier { get; set; }
       public string student_name { get; set; }
       public string email {get; set;}
       public string student_phoneNumber { get; set;}
       public string role {get; set;}
       public int person_credits {get; set;}
       public int needed_credits {get; set;}
       public List<SubjectEnrollments> enrollments { get; set;}    

    }
}
