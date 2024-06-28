using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVerServer.Abstractions;

namespace UniVerServer.Seed;

public class UserSeeding : BaseSeedingConfiguration<Users.Models.Users>
{
    public override void Configure(EntityTypeBuilder<Users.Models.Users> builder)
    {
        builder.HasData( 
            new Users.Models.Users
            {
                Id = Guid.Parse("14d52b1e-f5b3-4750-b327-56ddadbd1f78"),
                FirstNames = "Leander",
                LastNames = "van Aarde",
                IdentityNumber = "0103265065088",
                Identifier = "200211",
                PersonalEmail = "Leander.vaonline@gmail.com",
                ContactNumber = "0825678132",
                ModifiedDate = DateTime.UtcNow,
                IssuedEmail = "200211@virtualwindow.co.za",
                ProfileImage = "http://example.com/leander1.jpg",
                Credits = 0,
                RequiredCredits = 360,
                RoleId = Guid.Parse("58d25d4c-3b3a-4f52-b282-48e0f458fb79"),
                Active = true,
                Password = "Password2",
                PasswordModifiedDate = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            },

            new Users.Models.Users
            {
                Id = Guid.Parse("2881e5ba-daad-41f9-92f2-9d67dcc0bd00"),
                FirstNames = "Alex",
                LastNames = "Petterson",
                IdentityNumber = "0204157890123",
                Identifier = "210312",
                PersonalEmail = "alex.online@gmail.com",
                ContactNumber = "0834567890",
                ModifiedDate = DateTime.UtcNow,
                IssuedEmail = "210312@virtualwindow.co.za",
                ProfileImage = "http://example.com/alex1.jpg",
                Credits = 50,
                RequiredCredits = 400,
                RoleId = Guid.Parse("58d25d4c-3b3a-4f52-b282-48e0f458fb79"),
                Active = true,
                Password = "Password2",
                PasswordModifiedDate = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            },

            new Users.Models.Users
            {
                Id = Guid.Parse("497223a1-720a-408e-8a07-cfa7e48655b3"),
                FirstNames = "Jamie",
                LastNames = "Oliver",
                IdentityNumber = "0305278901234",
                Identifier = "220513",
                PersonalEmail = "jamie.online@gmail.com",
                ContactNumber = "0845678901",
                ModifiedDate = DateTime.UtcNow,
                IssuedEmail = "220513@virtualwindow.co.za",
                ProfileImage = "http://example.com/jamie1.jpg",
                Credits = 100,
                RequiredCredits = 500,
                RoleId = Guid.Parse("58d25d4c-3b3a-4f52-b282-48e0f458fb79"),
                Active = true,
                Password = "Password2",
                PasswordModifiedDate = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            }, 
            new Users.Models.Users
            {
                Id = Guid.Parse("49b27cdd-b59f-4a26-80a4-6f7a1c1b2fd7"),
                FirstNames = "Taylor",
                LastNames = "Swiftee",
                IdentityNumber = "0406389012345",
                Identifier = "230614",
                PersonalEmail = "taylor.online@gmail.com",
                ContactNumber = "0856789012",
                ModifiedDate = DateTime.UtcNow,
                IssuedEmail = "taylor.230614@virtualwindow.co.za",
                ProfileImage = "http://example.com/taylor1.jpg",
                Credits = 360,
                RequiredCredits = 360,
                RoleId = Guid.Parse("68b4d362-9271-4f9f-aa7b-6a9b1953dc3c"),
                Active = true,
                Password = "Password2",
                PasswordModifiedDate = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            },

            new Users.Models.Users
            {
                Id = Guid.Parse("4cfdf887-baf3-4847-a68a-18e7117beddc"),
                FirstNames = "Morgan",
                LastNames = "Freeman",
                IdentityNumber = "0507490123456",
                Identifier = "240715",
                PersonalEmail = "morgan.online@gmail.com",
                ContactNumber = "0867890123",
                ModifiedDate = DateTime.UtcNow,
                IssuedEmail = "morgan.240715@virtualwindow.co.za",
                ProfileImage = "http://example.com/morgan1.jpg",
                Credits = 360,
                RequiredCredits = 360,
                RoleId = Guid.Parse("68b4d362-9271-4f9f-aa7b-6a9b1953dc3c"),
                Active = true,
                Password = "Password2",
                PasswordModifiedDate = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            },

            new Users.Models.Users
            {
                Id = Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                FirstNames = "Jordan",
                LastNames = "Michael",
                IdentityNumber = "0608501234567",
                Identifier = "250816",
                PersonalEmail = "jordan.online@gmail.com",
                ContactNumber = "0878901234",
                ModifiedDate = DateTime.UtcNow,
                IssuedEmail = "jordan.250816@virtualwindow.co.za",
                ProfileImage = "http://example.com/jordan1.jpg",
                Credits = 360,
                RequiredCredits = 360,
                RoleId = Guid.Parse("68b4d362-9271-4f9f-aa7b-6a9b1953dc3c"),
                Active = true,
                Password = "Password2",
                PasswordModifiedDate = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            }
        ); 
    }
}