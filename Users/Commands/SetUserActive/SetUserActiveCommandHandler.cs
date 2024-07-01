using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Users.Commands.SetUserActive;

public class SetUserActiveCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<SetUserActiveCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(SetUserActiveCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var user = await _context.Users.FindAsync(request.id);
            if (user is null) throw new NotFoundException(request.id);
            user.Active = true;
            response = new ResponseDto(user.Id, "User updated", StatusCodes.Accepted);
            return response;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Something went wrong", StatusCodes.BadRequest);
            return response;
        }

    }
}