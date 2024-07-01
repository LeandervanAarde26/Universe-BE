namespace UniVerServer.Users.DTO;

public class LecturerInformation
{
    public Guid LecturerId {get; set;} 
    public string FullNames {get; set;} 
    public string Email {get; set;}
    public string ProfileImage {get; set;} 
    public bool Active {get; set;}  
}