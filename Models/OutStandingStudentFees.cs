using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVerServer.Models
{
    [Table("outstanding_student_fees")]
    public class OutStandingStudentFees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fee_id { get; set; }

        [Required]
        [ForeignKey("person_system_identifier")]
        public People student_id { get; set; }

        [Required]
        public int outstanding_amount { get; set; }

        [Required]
        public DateTime payment_date = DateTime.Now;
    }
}
