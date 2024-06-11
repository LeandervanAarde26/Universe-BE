using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using UniVerServer.Abstractions;

namespace UniVerServer.Subjects.Models;

[Table("Subjects")]
public class Subject : BaseModel
{
 // subject information
 [Required]
 public string Name { get; set; }
 public string Description { get; set; }
 public decimal Cost { get; set; }
 public int Credits { get; set; }
 
 public Users.Models.Users Lecturer { get; set; }
 public Guid LecturerId { get; set; }
 
 //Extras (nice to have)
 public string ColorHex { get; set; }
 public string Image { get; set; } = "http";
 
 // running information
 public DateTime StartDate { get; set; }
 public bool Active { get; set; }
 public int ClassRuntime { get; set; }
 public int ClassRepitions { get; set; }
}