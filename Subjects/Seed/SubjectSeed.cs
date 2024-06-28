using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVerServer.Abstractions;
using UniVerServer.Subjects.Enums;
using UniVerServer.Subjects.Models;

namespace UniVerServer.Subjects.Seed;

public class SubjectSeed : BaseSeedingConfiguration<Subject>
{
    public override void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasData(
            new Subject
            {
                Id = Guid.Parse("28384e02-4ac9-4098-89b7-405c465c730e"),
                Name = "Interactive Development",
                Description =
                    "Delve into the world of programming websites with Interactive Development blah blah blah",
                Cost = 13000.00m,
                Credits = 60,
                Year = 1,
                LecturerId = Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                Identifier = "IDV100",
                ColorHex = "#000000",
                Image = "http://example.com/interactive-development.jpg",
                Active = true,
                ClassRuntime = 120, // minutes
                ClassRepitions = 8,
                ClassDayIntervals = 7, // class once every 7 days
                Type = SubjectTypes.CORE,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            },

            new Subject
            {
                Id = Guid.Parse("2c5240e9-d233-45bb-8d9b-8d6e709b2937"),
                Name = "Data Structures and Algorithms",
                Description =
                    "Learn about the fundamental data structures and algorithms used in computer science blah blah blah",
                Cost = 15000.00m,
                Credits = 70,
                Year = 2,
                Identifier = "DSA200",
                LecturerId = Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                ColorHex = "#FF5733",
                Image = "http://example.com/data-structures.jpg",
                Active = true,
                ClassRuntime = 150, // minutes
                ClassRepitions = 10,
                ClassDayIntervals = 14, // class once every 14 days
                Type = SubjectTypes.CORE,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            },

            new Subject
            {
                Id = Guid.Parse("2ebc8d73-d02a-4c97-990a-6793dd0c7386"),
                Name = "Artificial Intelligence",
                Description =
                    "Explore the concepts and applications of Artificial Intelligence in various fields blah blah blah",
                Cost = 17000.00m,
                Credits = 80,
                Year = 3,
                Identifier = "AI304",
                LecturerId = Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                ColorHex = "#33FF57",
                Image = "http://example.com/ai.jpg",
                Active = true,
                ClassRuntime = 180, // minutes
                ClassRepitions = 12,
                ClassDayIntervals = 10, // class once every 10 days
                Type = SubjectTypes.MODULE,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            },

            new Subject
            {
                Id = Guid.Parse("2ebd49a2-8d97-4885-832b-8b2f950cbbd3"),
                Name = "Database Management Systems",
                Description = "Understand the principles of database design and management blah blah blah",
                Cost = 14000.00m,
                Credits = 65,
                Year = 2,
                Identifier = "DMS210",
                LecturerId = Guid.Parse("4cfdf887-baf3-4847-a68a-18e7117beddc"),
                ColorHex = "#3357FF",
                Image = "http://example.com/dbms.jpg",
                Active = true,
                ClassRuntime = 130, // minutes
                ClassRepitions = 9,
                ClassDayIntervals = 12, // class once every 12 days
                Type = SubjectTypes.CORE,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            },

            new Subject
            {
                Id = Guid.Parse("37d7ded2-6d83-4b49-ab75-f0bf5209589f"),
                Name = "Software Engineering",
                Description = "Gain insights into the software development lifecycle and methodologies blah blah blah",
                Cost = 16000.00m,
                Credits = 75,
                Year = 3,
                Identifier = "SE300",
                LecturerId = Guid.Parse("4cfdf887-baf3-4847-a68a-18e7117beddc"),
                ColorHex = "#FF33A5",
                Image = "http://example.com/software-engineering.jpg",
                Active = true,
                ClassRuntime = 160, // minutes
                ClassRepitions = 11,
                ClassDayIntervals = 9, // class once every 9 days
                Type = SubjectTypes.CORE,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            },

            new Subject
            {
                Id = Guid.Parse("39fe9dbe-592b-440c-8c80-686f5dd47327"),
                Name = "Computer Networks",
                Description = "Learn the fundamentals of computer networking and protocols blah blah blah",
                Cost = 12000.00m,
                Credits = 55,
                Year = 1,
                Identifier = "CNS200",
                LecturerId = Guid.Parse("4cfdf887-baf3-4847-a68a-18e7117beddc"),
                ColorHex = "#33FFB2",
                Image = "http://example.com/networks.jpg",
                Active = true,
                ClassRuntime = 110, // minutes
                ClassRepitions = 7,
                ClassDayIntervals = 8, // class once every 8 days
                Type = SubjectTypes.CORE,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            },

            new Subject
            {
                Id = Guid.Parse("45a5b291-98f8-4876-860a-99b918a1d24f"),
                Name = "Cybersecurity",
                Description = "Understand the key concepts and practices in cybersecurity blah blah blah",
                Cost = 18000.00m,
                Credits = 85,
                Year = 3,
                Identifier = "CS303",
                LecturerId = Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                ColorHex = "#FF9933",
                Image = "http://example.com/cybersecurity.jpg",
                Active = true,
                ClassRuntime = 200, // minutes
                ClassRepitions = 14,
                ClassDayIntervals = 6, // class once every 6 days
                Type = SubjectTypes.MODULE,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            }
        );
    }
}