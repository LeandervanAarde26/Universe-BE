using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVerServer.Models
{
    [Table("student_grades")]
    public class StudentCourses
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string grade_id { get; set; }

        [Required]
        [ForeignKey("subject_id")]
        public Subjects subject { get; set; }

        [Required]
        [ForeignKey("student_id")]
        public Users.Models.Users student { get; set; }

        [Required]
        [ForeignKey("facilitator_id")]
        public Users.Models.Users facilitator { get; set; }

        [Required]
        public int grade { get; set; } = 0;
    }
}
