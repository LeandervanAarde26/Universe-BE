using UniVerServer.Events.Enums;

namespace UniVerServer.Events.Dto;

public class UpdateEventDto
{
    public string Name { get; set; }    
    public string Description { get; set; }
    public string School { get; set; }
    public EventType Type { get; set; }
}