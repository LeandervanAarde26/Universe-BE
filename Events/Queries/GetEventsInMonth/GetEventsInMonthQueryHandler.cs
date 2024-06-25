using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Events.Dto;

namespace UniVerServer.Events.Queries.GetEventsInMonth;

public class GetEventsInMonthQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetEventsInMonthQuery, IEnumerable<GetEventsDto>>
{
    public async Task<IEnumerable<GetEventsDto>> Handle(GetEventsInMonthQuery request, CancellationToken cancellationToken)
    {
        try
        {
            int currentMonth = request.date.Month;
            var events = await _context.Events
                .Include(x => x.Organiser)
                .OrderBy(x => x.Date)
                .Where(x => x.Date.Month.Equals(currentMonth))
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