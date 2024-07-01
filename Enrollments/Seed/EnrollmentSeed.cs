using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.Enums;
using UniVerServer.Enrollments.Models;

namespace UniVerServer.Enrollments.Seed;

public class EnrollmentSeed : BaseSeedingConfiguration<Enrollment>
{
    public override void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasData(
            new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = Guid.Parse("14d52b1e-f5b3-4750-b327-56ddadbd1f78"),
                CourseId = Guid.Parse("f8a259ab-d2f3-482e-9074-842b4a097e27"),
                Status = EnrollmentStatus.INPROGRESS,
                Grade = 0.00m,
                GradeType = GradeType.INCOMPLETE,
                Modified = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            },
            new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = Guid.Parse("14d52b1e-f5b3-4750-b327-56ddadbd1f78"),
                CourseId = Guid.Parse("6c058d67-4d10-4fa6-8d6a-4f8b8c5a3a5e"),
                Status = EnrollmentStatus.INPROGRESS,
                Grade = 0.00m,
                GradeType = GradeType.INCOMPLETE,
                Modified = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            },
            new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = Guid.Parse("14d52b1e-f5b3-4750-b327-56ddadbd1f78"),
                CourseId = Guid.Parse("9fcbf95d-89a4-4e19-a828-3baf4aa75ec0"),
                Status = EnrollmentStatus.INPROGRESS,
                Grade = 0.00m,
                GradeType = GradeType.INCOMPLETE,
                Modified = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            },
            new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = Guid.Parse("14d52b1e-f5b3-4750-b327-56ddadbd1f78"),
                CourseId =  Guid.Parse("d3a2f2f0-8e7e-42e1-9f70-61f2a8c0b5ea"),
                Status = EnrollmentStatus.INPROGRESS,
                Grade = 0.00m,
                GradeType = GradeType.INCOMPLETE,
                Modified = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            },
        new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = Guid.Parse("2881e5ba-daad-41f9-92f2-9d67dcc0bd00"),
                CourseId = Guid.Parse("adff5e28-7a2d-4f71-8c1b-0b6d59a82390"),
                Status = EnrollmentStatus.INPROGRESS,
                Grade = 0.00m,
                GradeType = GradeType.INCOMPLETE,
                Modified = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow
            }
        );
    }
}