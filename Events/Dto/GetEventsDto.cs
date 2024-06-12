namespace UniVerServer.Events.Dto;

public class GetEventsDto
{
    public Guid Id { get; set; }
  
    public string Name { get; set; }    
    public string Description { get; set; }
    public string School { get; set; }
    public string Type { get; set; }
    public string Organisername { get; set; }
    public string Date { get; set; }
    public string DateCreated { get; set; }

}