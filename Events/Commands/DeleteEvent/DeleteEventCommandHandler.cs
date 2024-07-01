using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Events.Models;
using UniVerServer.Exceptions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Events.Commands.DeleteEvent;

public class DeleteEventCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<DeleteEventCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            Event deletableEvent = await _context.Events.FindAsync(request.id);
            if (deletableEvent is null)
                throw new NotFoundException($"Event with id {request.id} could not be found");

            _context.Events.Remove(deletableEvent);
            await context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(request.id, "Deleted event", StatusCodes.Accepted);
            return response;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Could not delete Event", StatusCodes.BadRequest);
            return response;
        }
    }
}