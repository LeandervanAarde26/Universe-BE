namespace UniVerServer.Models.CustomDataObjects
{
    public class CourseEnrollmentView
    {
        public int student_id { get; set; }
        public string student_name { get; set; }
        public string student_number { get; set; }
        public string student_systemIdentifier { get; set; }
        public string student_email { get; set;}
        public string student_phone { get; set;}
        public int student_credits { get; set;}
        public int student_needed_credits { get; set;}   
        //Lecturer information
        public int lecturer_id { get;set; }
        public string lecturer_name { get; set; }
        public int lecturer_rate { get; set;}
        //Subject information 
        public int subject_id { get; set; }
        public string subject_name { get; set;}
        public string subject_code { get; set;}
        public int subject_cost { get; set;}
        public string subject_description { get; set;}
        public string subject_color { get; set;}
        public int subject_credits { get; set;}
        public int subject_runtime { get; set;}
        public int class_amount { get; set; }
        public bool subject_active { get; set; }
        public DateTime subject_start { get; set; }
    }
}
