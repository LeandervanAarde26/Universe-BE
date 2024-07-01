using System.ComponentModel.DataAnnotations;

namespace UniVerServer.Abstractions;

public abstract class BaseModel
    // Something to consider: https://dba.stackexchange.com/questions/264/guid-vs-int-which-is-better-as-a-primary-key
    // Proposal: Perhaps the identifier is a better option to use when querying the table, however, 
    // The function however, can be flawed, this needs further investigation, does generating this new GUID have pros over the identifier? 
    // Should another identifier be added (int?) for querying? 
    // If I use the current Identifier{get; set;} field, will it take more compute resources to ensure it remains unique? 
    // Is this something I can rely on the database to do? Adding a unique constraint? 
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    public string Identifier { get; set; } = "VOID";
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}