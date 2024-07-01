using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniVerServer.Abstractions;
using UniVerServer.Subjects.Models;

namespace UniVerServer.Courses.Models;

[Table("Courses")]
public class Course : BaseModel
{
    public Subject Subject { get; set; }
    public Guid SubjectId { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public bool Active { get; set; } = false;
    public bool AcceptingStudents { get; set; } = false;
}