using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVerServer.Models
{
    [Table("subjects")]
    public class Subjects
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int subject_id { get; set; }
        [Required]
        public string subject_name { get; set; } = String.Empty;

        [Required]
        public string subject_code { get; set; } = String.Empty;

        [Required]
        public string subject_description { get; set; } = String.Empty;

        [Required]
        public int subject_cost { get; set; } = 0;


        [Required]
        public string subject_color { get; set; } = String.Empty;

        [ForeignKey("person_id")]
        [Required]
        public int lecturer_id { get; set; }

        [Required]
        public int subject_credits { get; set; } = 0;
        [Required]
        public int subject_class_runtiem { get; set; } = 0;


        [Required]
        public int subject_class_amount { get; set; } = 0;

        public string subjectImage { get; set; } = String.Empty;

        public bool is_active { get; set; }

        public DateTime course_start { get; set; } = DateTime.Now;

    }
}
