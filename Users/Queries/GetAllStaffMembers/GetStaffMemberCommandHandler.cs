using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using UniVerServer.Users.DTO;
using UniVerServer.Users.Mapping;

namespace UniVerServer.Users.Queries.GetAllStaffMembers;

public class GetStaffMemberCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetStaffMemberQuery, IEnumerable<GetStaffMembersDto>>
{   
    public async Task<IEnumerable<GetStaffMembersDto>> Handle(GetStaffMemberQuery request,
        CancellationToken cancellationToken)
    {
        
        var config = new MapperConfiguration(cfg => cfg.AddProfile<UserMapping>());
        var mapper = new Mapper(config);
        try
        {

            var users = await _context.Users
                .Include(x => x.Role)
                .Where(y => request.RoleName.Equals("*")
                    ? y.Role.Name != "Degree" && y.Role.Name != "Certificate"
                    : y.Role.Name.Equals(request.RoleName) && y.Role.Name != "Degree" && y.Role.Name != "Certificate"
                )
                .ToListAsync(cancellationToken);
            
            if (users.IsNullOrEmpty())
            {
                throw new NotFoundException(new Guid());
            }
            
            var userDtos = mapper.Map<IEnumerable<GetStaffMembersDto>>(users);
            return userDtos;
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