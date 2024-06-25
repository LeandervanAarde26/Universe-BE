namespace UniVerServer.Events.Dto;

public class UpdateEventHostDto
{
    public Guid EventId { get; set; }
    public Guid NewHost { get; set; }
}