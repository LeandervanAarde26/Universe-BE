using UniVerServer.Events.Enums;

namespace UniVerServer.Events.Dto;

public class CreateEventDto
{
    public string Name { get; set; }    
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string School { get; set; }
    public Guid OrganiserId { get; set; }
    public EventType Type { get; set; }
}