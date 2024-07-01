using MediatR;
using UniVerServer.Abstractions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<DeleteUserCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var personToDelete = await _context.Users.FindAsync(request.Id);
            if (personToDelete == null)
            {
                response = new ResponseDto(default, $"{request.Id} could not be found", StatusCodes.NotFound);
                return response;
            }

            if (!personToDelete.Active)
            {
                response = new ResponseDto(default, "Person is already inactive", StatusCodes.Conflict);
                return response;
            }
            personToDelete.Active = false;
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(personToDelete.Id, "User Deleted", StatusCodes.Accepted);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Something went wrong..", StatusCodes.BadRequest);
            return response;
        }
        
    }
}