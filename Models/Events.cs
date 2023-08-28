using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniVerServer.Models
{
    [Table("events")]
    public class Events
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int event_id { get; set; }
  
        [Required]
        public string event_name { get; set; }

        [Required]
        public string event_description { get; set; }

        [ForeignKey("person_id")]
        [Required]
        public int staff_organiser { get; set; }

        [Required]
        public DateTime event_date { get; set; } = DateTime.UtcNow;
    }
}
