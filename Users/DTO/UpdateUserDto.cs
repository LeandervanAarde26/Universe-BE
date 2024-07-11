
namespace UniVerServer.Users.DTO;

public class UpdateUserDto
{

    public string FirstNames { get; set; }
    public string LastNames { get; set; }
    public string PersonalEmail { get; set; }
    public string ContactNumber { get; set; }
    public DateTime ModifiedDate { get; private set; }
    public string ProfileImage { get; set; } = "http";
    public int Credits { get; set; } = 0;
    public int RequiredCredits { get; private set; }

    public UpdateUserDto()
    {
        ModifiedDate = DateTime.UtcNow;
        
    }
    
    public void SetRequiredCredits(int requiredCredits)
    {
        RequiredCredits = requiredCredits;
    }
}