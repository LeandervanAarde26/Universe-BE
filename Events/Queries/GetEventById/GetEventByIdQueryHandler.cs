using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Events.Dto;
using UniVerServer.Events.Mapping;
using UniVerServer.Exceptions;

namespace UniVerServer.Events.Queries.GetEventById;

public class GetEventByIdQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetEventByIdQuery, GetSingleEventDto>
{
    public async Task<GetSingleEventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<EventsMapping>());
        var mapper = new Mapper(config);
        try
        {
            var ev = await _context.Events
                .Include(x => x.Organiser)
                .Where(y => y.Id.Equals(request.id))
                .FirstOrDefaultAsync(cancellationToken);
            if (ev is null)
                throw new NotFoundException($"Event with id {request.id} does not exist");
            var mappedEvent = mapper.Map<GetSingleEventDto>(ev);
            return mappedEvent;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}