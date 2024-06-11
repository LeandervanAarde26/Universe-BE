using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Users.Commands.UpdatePhoneNumber;

public class UpdatePhoneNumberCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateUserPhoneNumberCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateUserPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var user = await _context.Users.FindAsync(request.fields.Id);
            if (user is null)
            {
                throw new NotFoundException(request.fields.Id);
            }
            
            user.ContactNumber = request.fields.ContactNumber;
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(user.Id, "User Updated", StatusCodes.Accepted);
            return response;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Cannot update user \n \n {e.Message} \n \n", StatusCodes.BadRequest);
            return response;
        }
    }
}