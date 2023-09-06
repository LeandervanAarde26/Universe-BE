using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVerServer.Models
{
    [Table("student_payments")]
    public class MadePayments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int payment_id { get; set; }

        [Required]
        [ForeignKey("person_system_identifier")]
        public int person_id { get; set; }

        [Required]
  
        public int payment_amount { get; set; } = 0;

        [Required]
     
        public DateTime payment_date { get; set; } = DateTime.Now;

    }
}
