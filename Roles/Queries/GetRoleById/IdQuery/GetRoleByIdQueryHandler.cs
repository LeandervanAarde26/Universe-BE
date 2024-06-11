using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;

namespace UniVerServer.Roles.Queries.GetRoleById.IdQuery;

public class GetRoleByIdQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetRoleByIdQuery, Models.Roles>
{
    public async Task<Models.Roles> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var role = await _context.Roles.FindAsync(request.id);
            if (role is null)
                throw new NotFoundException($"Can not find role with ID {request.id}");
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