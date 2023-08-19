using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using UniVerServer.Models;

namespace UniVerServer.Models
{
    [Table("people")]
    public class People
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int person_id { get; set; }

        [Required]
        [StringLength(20)]
        public string person_system_identifier { get; set; } = string.Empty;
   
        [Required]
        [StringLength(60)]
        public string first_name { get; set; } = string.Empty;

        [Required]
        [StringLength(60)]
        public string last_name { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        public string person_email { get; set; } = string.Empty;

        [Required]
        public DateTime added_date { get; set; } = DateTime.Now;

        [Required]
        public bool person_active { get; set; } = false;

        [Required]
        [ForeignKey("role_id")]
        public int role { get; set; } = 3;

        public string person_image = string.Empty;

        public int person_credits { get; set; }

        public int needed_credits { get; set; }

        public string person_password { get; set; }

    }
}
