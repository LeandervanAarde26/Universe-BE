namespace UniVerServer.Users.DTO;

public class GetstudentsDto
{
    public Guid Id { get; set; }
    public string Identifier { get; set; }
    public string DateCreated { get; set; }
    public string FirstNames { get; set; }
    public string LastNames { get; set; }
    public string IdentityNumber { get; set; }
    public string PersonalEmail { get; set; }
    public string ContactNumber { get; set; }
    // School required information
    public DateTime ModifiedDate { get; set; }
    public string IssuedEmail { get; set; }
    public string ProfileImage { get; set; } = "http";
    public int Credits { get; set; } = 0;
    public int RequiredCredits { get; set; }
    // Auth information
    public string Role { get; set; }
    public bool Active { get; set; }


    public GetstudentsDto(DateTime createdDate, string role)
    {
        DateCreated = createdDate.ToShortDateString();
        Role = role;
    }
}