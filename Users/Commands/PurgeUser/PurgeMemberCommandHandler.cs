using MediatR;
using UniVerServer.Abstractions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Users.Commands.PurgeUser;

public class PurgeMemberCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<PurgeUserCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(PurgeUserCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var personToDelete = await _context.Users.FindAsync(request.Id);
            if (personToDelete == null)
            {
                response = new ResponseDto(default, $"{request.Id} could not be found", Enums.StatusCodes.NotFound);
                return response;
            }

            if (personToDelete.Active)
            {
                response = new ResponseDto(default, "Can not delete user who is active", Enums.StatusCodes.Conflict);
                return response;
            }
            _context.Users.Remove(personToDelete);
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(personToDelete.Id, "User Purged", Enums.StatusCodes.Accepted);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Something went wrong..", StatusCodes.BadRequest);
            return response;
        }
    }
}