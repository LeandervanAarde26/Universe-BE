using AutoMapper;
using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Events.Mapping;
using UniVerServer.Events.Models;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateEventCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<EventsMapping>());
        var mapper = new Mapper(config);
        try
        {
            Event eventToUpdate = await context.Events.FindAsync(request.eventId);
            if (eventToUpdate is null)
            {
                response = new ResponseDto(default, $"Event with id: {request.eventId} could not be found", StatusCodes.NotFound);
                return response;
            }
            
            mapper.Map(request.ev, eventToUpdate);
            await _context.SaveChangesAsync(cancellationToken);

            response = new ResponseDto(request.eventId, "Event updated", StatusCodes.Ok);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Something went wrong: {e.Message}", StatusCodes.BadRequest);
            return response;
        }
    }
}