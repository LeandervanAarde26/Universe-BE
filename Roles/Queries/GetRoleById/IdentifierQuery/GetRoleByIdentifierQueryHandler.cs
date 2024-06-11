using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;

namespace UniVerServer.Roles.Queries.GetRoleById.IdentifierQuery;

public class GetRoleByIdentifierQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetRoleByIdentifierQuery, Models.Roles>
{
    public async Task<Models.Roles> Handle(GetRoleByIdentifierQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Identifier == request.identifier,
                cancellationToken);
            if (role is null)
                throw new NotFoundException($"Can not find role with identifier {request.identifier}");
            return role;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}