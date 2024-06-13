using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Events.Models;
using UniVerServer.Exceptions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Events.Commands.UpdateDate;

public class UpdateEventDateCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateEventDateCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateEventDateCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            Event eventToUpdate = await _context.FindAsync<Event>(request.id);
            if (eventToUpdate is null)
                throw new NotFoundException($"Event with Id {request.id} does not exist");
            eventToUpdate.Date = request.date;
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(request.id, "Event updated", StatusCodes.Accepted);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Could not update Event", StatusCodes.BadRequest);
            return response;
        }
    }
}