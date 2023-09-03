namespace UniVerServer.Models.DTO
{
    public class SingleSubjectView
    {
        public int student_id { get; set;}
        public string student_name { get; set;}
        public string student_email { get; set;}
        public int lecturer_id { get; set;}
        public string lecturer_name { get; set;}
        public int subject_id { get; set;}
        public string subject_name { get; set;}
        public string subject_color { get; set;}
        public bool subject_active { get; set;}
        public string subject_description { get; set;}
        public string subject_code { get; set;}
        public int enrollment_id { get; set; }
    }

    public class Enrollment
    {
        public int student_id { get; set; }
        public string student_name { get; set; }
        public string student_email { get; set; }
        public int enrollment_id { get; set; }
    }

    public class SubjectWithEnrollments
    {
        public string subjectName { get; set; }
        public string subjectDescription { get; set; }
        public int lecturer_id { get; set; }
        public string lecturer_name { get; set; }
        public int subjectId { get; set; }
        public string subject_code { get; set; }
        public string subject_color { get; set; }
        public bool subject_active { get; set; }
        public List<Enrollment> enrollments { get; set; }
    }
}
