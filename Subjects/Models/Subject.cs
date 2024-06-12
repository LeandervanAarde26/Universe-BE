using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using UniVerServer.Abstractions;
using UniVerServer.Subjects.Enums;

namespace UniVerServer.Subjects.Models;

[Table("Subjects")]
public class Subject : BaseModel
{
 // subject information
 [Required]
 public string Name { get; set; }
 public string Description { get; set; }
 public decimal Cost { get; set; }
 public int Credits { get; set; } = 20;
 public int Year { get; set; }
 
 public Users.Models.Users Lecturer { get; set; }
 public Guid LecturerId { get; set; } 
 
 //Extras (nice to have)
 public string ColorHex { get; set; } = "#000000";
 public string Image { get; set; } = "http";
 
 // running information
 public bool Active { get; set; } = false;
 public int ClassRuntime { get; set; } = 120;
 public int ClassRepitions { get; set; } = 8;
 public SubjectTypes Type { get; set; }
 public DateTime DateModified { get; set; }
}