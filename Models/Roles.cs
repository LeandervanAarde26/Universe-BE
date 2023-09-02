using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniVerServer.Models
{
    [Table("people_roles")]
    public class Roles
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int role_id { get; set; } = 0;
        [Required]
        [StringLength(30)]
        public string role_name { get; set; } = string.Empty;
        [Required]
        public bool can_access { get; set; } = false;
        [Required]
        public int rate { get; set; }
    }
}
