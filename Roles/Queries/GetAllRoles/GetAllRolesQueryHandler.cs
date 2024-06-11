using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;

namespace UniVerServer.Roles.Queries.GetAllRoles;

public class GetAllRolesQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetRolesQuery, IEnumerable<Models.Roles>>
{
    public async Task<IEnumerable<Models.Roles>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _context.Roles.ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}