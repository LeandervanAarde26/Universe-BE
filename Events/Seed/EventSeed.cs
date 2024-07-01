using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVerServer.Abstractions;
using UniVerServer.Events.Enums;
using UniVerServer.Events.Models;

namespace UniVerServer.Events.Seed;

public class EventSeed : BaseSeedingConfiguration<Event>
{
    public override void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasData(
            new Event
            {
                Id = Guid.Parse("3f1a17e5-1c8e-4f6b-a5f7-27c5e9d6a41d"),
                Name = "Open Day - Winter",
                Description = "Come join us at our open day and explore our winter programs and activities.",
                Date = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                School = "CREATIVE TECH",
                OrganiserId = Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                Type = EventType.SOCIAL
            },

            new Event
            {
                Id = Guid.Parse("a2b92fe1-5918-42f4-957a-0e57c7b3e38b"),
                Name = "Open Day - Spring",
                Description = "Come join us at our open day and see what we have planned for the spring semester.",
                Date = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                School = "CREATIVE TECH",
                OrganiserId =  Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                Type = EventType.SOCIAL
            },

            new Event
            {
                Id = Guid.Parse("5d0e2f6c-8d20-4b0a-bf65-77e4ec12b179"),
                Name = "Open Day - Summer",
                Description = "Come join us at our open day and learn more about our summer courses and events.",
                Date = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                School = "CREATIVE TECH",
                OrganiserId = Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                Type = EventType.SOCIAL
            },

            new Event
            {
                Id = Guid.Parse("c0840d57-4d73-4f7c-afe6-68b79bc01660"),
                Name = "Open Day - Autumn",
                Description = "Come join us at our open day and discover our autumn curriculum and programs.",
                Date = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                School = "CREATIVE TECH",
                OrganiserId = Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                Type = EventType.SOCIAL
            },

            new Event
            {
                Id =Guid.Parse("e5fe9368-1f62-4d5d-bf98-3c8d12a8764b"),
                Name = "Open Day - New Year",
                Description = "Come join us at our open day and start the new year with exciting courses and opportunities.",
                Date = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                School = "CREATIVE TECH",
                OrganiserId =  Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                Type = EventType.SOCIAL
            },

            new Event
            {
                Id = Guid.Parse("b12a9fb8-42cf-4d0a-8a2d-d980dd9ff1e1"),
                Name = "Open Day - Mid-Year",
                Description = "Come join us at our open day and see what we have in store for the mid-year term.",
                Date = DateTime.UtcNow.AddMonths(new Random().Next(0, 4)),
                School = "CREATIVE TECH",
                OrganiserId =  Guid.Parse("59332d00-80a2-446c-afca-00a3160c0764"),
                Type = EventType.SOCIAL
            }
        );
    }
}