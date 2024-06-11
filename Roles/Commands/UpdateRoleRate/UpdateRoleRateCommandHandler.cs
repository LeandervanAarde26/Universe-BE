using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Roles.Commands.UpdateRoleRate;

public class UpdateRoleRateCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateRoleRateCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateRoleRateCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var roleToUpdate = await _context.Roles.FindAsync(request.id);
            if (roleToUpdate is null)
                throw new NotFoundException("Specified role does not exist");
            roleToUpdate.HourlyRate = request.newRate;
            await context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(default, "Role updated", StatusCodes.Accepted);
            return response;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Cannot complete operation", StatusCodes.BadRequest);
            return response;
        }
    }
}