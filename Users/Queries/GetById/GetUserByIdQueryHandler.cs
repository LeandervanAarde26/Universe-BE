using AutoMapper;
using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using UniVerServer.Users.DTO;
using UniVerServer.Users.Mapping;

namespace UniVerServer.Users.Queries.GetById;

public class GetUserByIdQueryHandler(ApplicationDbContext context):
    BaseHandler(context), IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
{
    public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<UserMapping>());
        var mapper = new Mapper(config);
        try
        {
            var user = await _context.Users.FindAsync(request.Id);
            user.Role  = await _context.Roles.FindAsync(user.RoleId);
            
            if (user is null)
            {
                throw new NotFoundException(request.Id);
            }
            var userDto = mapper.Map<GetUserByIdDto>(user);
            return userDto;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}