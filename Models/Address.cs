using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace UniVerServer.Models
{
    [Table("people_address")]
    public class Address
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int address_id { get; set; } = 0;
        [Required]
        [StringLength(50)]
        public string address { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string address_province { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string address_city { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string address_area { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string address_zipcode { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string address_phone { get; set; } = String.Empty;

    }
}
