namespace UniVerServer.Models.DTO
{
    public class LecturerPayment
    {
        public int subject_id { get; set; }
        public string subject_name { get; set; }
        public int lecturer_id { get; set; }
        public string lecturer { get; set; }
        public decimal subject_class_amount { get; set; }
        public DateTime course_start { get; set; }
        public decimal rate { get; set; }
        public decimal monthlyIncome { get; set; }
        public int class_time { get; set; }
        public decimal hoursWorked { get; set; }    
    }


    public class CollectiveLecturerSalary
    {
        public int lecturerId { get; set; }
        public string lecturer { get; set; }
        public decimal totalHoursWorked { get; set; }
        public decimal monthlyIncome { get; set; }
    }
}
