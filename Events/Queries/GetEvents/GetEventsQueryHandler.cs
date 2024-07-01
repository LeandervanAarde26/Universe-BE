using System.Collections;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Events.Dto;
using UniVerServer.Events.Mapping;

namespace UniVerServer.Events.Queries.GetEvents;

public class GetEventsQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetEventsQuery, IEnumerable<GetEventsDto>>
{
    public async Task<IEnumerable<GetEventsDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var events = await _context.Events
                .Include(x => x.Organiser)
                .OrderBy(x => x.Date)
                .Select( x => new GetEventsDto
                    {
                     Id = x.Id,
                     Name = x.Name,   
                     Description = x.Description,
                     School = x.School,
                     Type = x.Type.ToString(),
                     Organisername = $"{x.Organiser.FirstNames} {x.Organiser.LastNames}",
                     Date = x.Date.ToShortDateString(),
                     DateCreated = x.DateCreated.ToShortDateString()
                    }
                )
              
                .ToListAsync(cancellationToken);

            return events;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}