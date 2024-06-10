using System.ComponentModel.DataAnnotations;

namespace UniVerServer.Abstractions;

public abstract class BaseModel
{
    [Key]
    [Required]
    public Guid Id { get; set; }
    public string Identifier { get; set; }
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}