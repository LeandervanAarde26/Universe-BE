using System.Collections;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using UniVerServer.Users.DTO;
using UniVerServer.Users.Mapping;

namespace UniVerServer.Users.Queries.GetAllStudents;

public class GetStudentsQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetStudentsQuery, IEnumerable<GetstudentsDto>>
{
    public async Task<IEnumerable<GetstudentsDto>> Handle(GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        
        var config = new MapperConfiguration(cfg => cfg.AddProfile<UserMapping>());
        var mapper = new Mapper(config);
        try
        {

            var users = await _context.Users
                .Include(x => x.Role)
                .Where(y => request.RoleName.Equals("*")
                    ? y.Role.Name != "Staff" && y.Role.Name != "Admin" && y.Role.Name != "Lecturer"
                    : y.Role.Name.Equals(request.RoleName) 
                      && y.Role.Name != "Staff" 
                      && y.Role.Name != "Admin"
                      && y.Role.Name != "Lecturer"
                      
                )
                .ToListAsync(cancellationToken);
            
            if (users.IsNullOrEmpty())
            {
                throw new NotFoundException(new Guid());
            }
            
            var userDtos = mapper.Map<IEnumerable<GetstudentsDto>>(users);
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