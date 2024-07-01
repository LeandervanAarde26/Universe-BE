using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using UniVerServer.Extensions;
using UniVerServer.Users.Commands.UpdateUser;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Users.Commands.UpdatePassword;

public class UpdatePasswordCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateUserPasswordCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var user = await _context.Users.FindAsync(request.id);
            if (user is null) throw new NotFoundException(request.id);
            user.Password = request.password.HashPassword();
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(user.Id, "User password updated", StatusCodes.Accepted);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Could not update user", StatusCodes.BadRequest);
            return response;
        }
    }
}