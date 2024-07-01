using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Roles.Commands.PurgeRole;

public class PurgeRoleCommandHandler(ApplicationDbContext context) : BaseHandler(context), IRequestHandler<PurgeRoleCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(PurgeRoleCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;

        // Start a transaction
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            // Get the role to remove
            var roleToRemove = await _context.Roles.FindAsync(request.id);
            if (roleToRemove is null)
                throw new NotFoundException("Role could not be found");

            // Get the default role
            var defaultRole = await _context.Roles.FirstOrDefaultAsync(x => x.Name.Equals("UNASSIGNED"), cancellationToken);
            if (defaultRole is null)
                throw new NotFoundException("Default role 'UNASSIGNED' could not be found");

            // Get users with the role to be removed
            var usersWithRole = await _context.Users.Where(x => x.RoleId == roleToRemove.Id).ToListAsync(cancellationToken);

            // Update users with the default role
            foreach (var user in usersWithRole)
            {
                user.RoleId = defaultRole.Id;
                _context.Users.Update(user);
            }

            // Save changes to update users
            await _context.SaveChangesAsync(cancellationToken);

            // Remove the role
            _context.Roles.Remove(roleToRemove);
            
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            // Return response
            response = new ResponseDto(roleToRemove.Id, "Role has been removed", StatusCodes.Accepted);
        }
        catch (NotFoundException)
        {
            // Rollback transaction in case of NotFoundException
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            response = new ResponseDto(default, $"Could not delete role: {e.Message}", StatusCodes.BadRequest);
        }

        return response;
    }
}
