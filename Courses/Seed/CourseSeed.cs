using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVerServer.Abstractions;
using UniVerServer.Courses.Extensions;
using UniVerServer.Courses.Models;

namespace UniVerServer.Courses.Seed;

public class CourseSeed : BaseSeedingConfiguration<Course>
{
    public override void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasData(
            new Course
            {
                Id = Guid.Parse("f8a259ab-d2f3-482e-9074-842b4a097e27"),
                SubjectId = Guid.Parse("28384e02-4ac9-4098-89b7-405c465c730e"),
                StartDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                EndDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)).CalculateEndDate(12, 10)
            },
            new Course
            {
                Id = Guid.Parse("6c058d67-4d10-4fa6-8d6a-4f8b8c5a3a5e"),
                SubjectId = Guid.Parse("2c5240e9-d233-45bb-8d9b-8d6e709b2937"),
                StartDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                EndDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)).CalculateEndDate(12, 10)
            },
            new Course
            {
                Id = Guid.Parse("9fcbf95d-89a4-4e19-a828-3baf4aa75ec0"),
                SubjectId = Guid.Parse("2ebd49a2-8d97-4885-832b-8b2f950cbbd3"),
                StartDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                EndDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)).CalculateEndDate(12, 10)
            },
            new Course
            {
                Id = Guid.Parse("d3a2f2f0-8e7e-42e1-9f70-61f2a8c0b5ea"),
                SubjectId =Guid.Parse("2ebc8d73-d02a-4c97-990a-6793dd0c7386"),
                StartDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                EndDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)).CalculateEndDate(12, 10)
            },
            new Course
            {
                Id = Guid.Parse("adff5e28-7a2d-4f71-8c1b-0b6d59a82390"),
                SubjectId = Guid.Parse("2ebd49a2-8d97-4885-832b-8b2f950cbbd3"),
                StartDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                EndDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)).CalculateEndDate(12, 10)
            },
            new Course
            {
                Id = Guid.Parse("7a3b56c6-1090-4f14-973f-b9b9d4b707cb"),
                SubjectId = Guid.Parse("37d7ded2-6d83-4b49-ab75-f0bf5209589f"),
                StartDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                EndDate = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)).CalculateEndDate(12, 10)
            }
        );
    }
}
