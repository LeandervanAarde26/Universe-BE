using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Events.Mapping;
using UniVerServer.Events.Models;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Events.Commands.CreateEvent;

public class CreateEventCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<CreateEventCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<EventsMapping>());
        var mapper = new Mapper(config);
        try
        {
            bool hasDuplicateEvent = await _context.Events.AnyAsync(x => x.Name.Equals(request.ev.Name)
                                                                        && x.OrganiserId.Equals(request.ev.OrganiserId)
                                                                            && x.Date.Equals(request.ev.Date));
            if (hasDuplicateEvent)
            {
                response = new ResponseDto(default, "Event already exists", StatusCodes.Conflict);
                return response;
            }

            var eventity = mapper.Map<Event>(request.ev);
            _context.Add(eventity);
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(default, "Event created", StatusCodes.Accepted);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Could not add event", StatusCodes.BadRequest);
            return response;
        }
    }
}