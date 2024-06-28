using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVerServer.Abstractions;

namespace UniVerServer.Roles.Seed;

public class RoleSeed : BaseSeedingConfiguration<Models.Roles>
{
    public override void Configure(EntityTypeBuilder<Models.Roles> builder)
    {
        builder.HasData(
            new Models.Roles
            {
                Id = Guid.Parse("58d25d4c-3b3a-4f52-b282-48e0f458fb79"),
                Name = "Degree",
                CanAccess = false, 
                PaidRole = false, 
                HourlyRate = 0.00m,
                Identifier = "R1",
                DateCreated = DateTime.UtcNow
            },
            new Models.Roles
            {
                Id = Guid.Parse("68b4d362-9271-4f9f-aa7b-6a9b1953dc3c"),
                Name = "Lecturer",
                CanAccess = false, 
                PaidRole = true, 
                HourlyRate = 20.00m,
                Identifier = "R2",
                DateCreated = DateTime.UtcNow
            },
            new Models.Roles
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-7a89-b0c1-2d3e4f567890"),
                Name = "Admin",
                CanAccess = true, 
                PaidRole = false, 
                HourlyRate = 50.00m,
                Identifier = "R3",
                DateCreated = DateTime.UtcNow
            },
            new Models.Roles
            {
                Id = Guid.Parse("a9e3c834-7182-4b3a-80e2-b28a9a7bce42"),
                Name = "Certificate",
                CanAccess = false, 
                PaidRole = false, 
                HourlyRate = 0.00m,
                Identifier = "R4",
                DateCreated = DateTime.UtcNow
            }
        );
    }
}