using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.Models;

namespace UniVerServer.Users.Models;

[Table("Users")]
public class Users : BaseModel
{
    // Personal information 
    [Required]
    public string FirstNames { get; set; }
    [Required]
    public string LastNames { get; set; }
    [Required]
    public string IdentityNumber { get; set; }
    [Required]
    public string PersonalEmail { get; set; }
    [Required]
    public string ContactNumber { get; set; }
    
     // School required information
    public DateTime ModifiedDate { get; set; }
    [Required]
    public string IssuedEmail { get; set; }
    public string ProfileImage { get; set; } = "http";
    [Required]
    public int Credits { get; set; } = 0;
    public int RequiredCredits { get; set; }
    
    // Auth information
    public Roles.Models.Roles Role { get; set; }
    public Guid RoleId { get; set; }
    [Required] 
    public bool Active { get; set; } = true;
    [Required]
    public string Password { get; set; }
    public DateTime PasswordModifiedDate { get; set; }
    
    public ICollection<Enrollment> Enrollments { get; set; }
}