using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;
using UniVerServer.Abstractions;
using UniVerServer.Events.Enums;

namespace UniVerServer.Events.Models;

[Table("Events")]
public class Event: BaseModel
{
    [Required]
    public string Name { get; set; }    
    public string Description { get; set; }
    [Required] public DateTime Date { get; set; } = DateTime.UtcNow;
    public string School { get; set; }
    public Users.Models.Users Organiser { get; set; }
    public Guid OrganiserId { get; set; }
    [Required]
    public EventType Type { get; set; }
}