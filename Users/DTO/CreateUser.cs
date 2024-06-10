using Isopoh.Cryptography.Argon2;
using Microsoft.Build.Framework;
using UniVerServer.Models;

namespace UniVerServer.Users.DTO;

public class CreateUserDto{
    
    [Required]
    public string FirstNames { get; set; }
    [Required]
    public string LastNames { get; set; }
    [Required]
    public string IdentityNumber { get; set; }
    [Required]
    public string PersonalEmail { get; set; }
    [Required]
    public string ContactNumber { get; set; }
    
    public DateTime ModifiedDate { get; private set; }
    public string IssuedEmail { get; private set; }
    public string Identifier { get; private set; }
    public string ProfileImage { get; private set; } = "http";
    public int Credits { get; set; } = 0;
    public int RequiredCredits { get; private set; }
    public string RoleId { get; set; }
    public bool Active { get; set; } = true;
    [Required]
    public string Password { get; private set; }
    public DateTime PasswordModifiedDate { get; private set; }
    
    public CreateUserDto(
        string firstNames,
        string lastNames,
        string identityNumber,
        string personalEmail,
        string contactNumber ,
        string roleId,
        int requiredCredits)
    {
        FirstNames = firstNames;
        LastNames = lastNames;
        IdentityNumber = identityNumber;
        PersonalEmail = personalEmail;
        ContactNumber = contactNumber;
        RequiredCredits = requiredCredits;

        RoleId = roleId;
        //Privately set fields
        Identifier = GenerateStudentNumber();
        IssuedEmail = GenerateStudentEmail(Identifier);
        Password = HashPassword("Op3nW1nd0w@dm1n");
        ModifiedDate = DateTime.UtcNow;
    }

    private string GenerateStudentNumber()
    {
        var random = new Random();
        var CurrentYear = DateTime.Now.Year.ToString().Substring(2);
        int randomDigits = random.Next(0, 10000);
        string randomPart = randomDigits.ToString("D4");
        var identifier = $"{CurrentYear}{randomPart}";
        return identifier;
    }
    
    private string GenerateStudentEmail(string studentNumber) =>
        $"{studentNumber}@virtualwindow.co.za";

    private string HashPassword(string password) =>
        Argon2.Hash(password);
}