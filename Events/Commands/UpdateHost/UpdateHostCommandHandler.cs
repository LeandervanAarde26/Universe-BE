using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Events.Models;
using UniVerServer.Exceptions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Events.Commands.UpdateHost;

public class UpdateHostCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateHostCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateHostCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            Event eventToUpdate = await _context.Events.FirstAsync(x => x.Id.Equals(request.data.EventId));
            if (eventToUpdate is null)
            {
                response = new ResponseDto(default, "Event was not found", StatusCodes.NotFound);
                return response;
            }

            bool userExists = await _context.Users.AnyAsync(x => x.Id.Equals(request.data.NewHost));

            if (!userExists)
            {
                response = new ResponseDto(default, $"User with id {request.data.NewHost} does not exist", StatusCodes.NotFound);
                return response;
            }

            eventToUpdate.OrganiserId = request.data.NewHost;

            await _context.SaveChangesAsync(cancellationToken);

            response = new ResponseDto(default, "Event has been updated", StatusCodes.Ok);
            return response;

        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Something went wronng : {e.Message}", StatusCodes.BadRequest);
            return response;
        }
    }
}