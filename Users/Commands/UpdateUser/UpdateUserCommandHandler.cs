using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using UniVerServer.Users.Mapping;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(ApplicationDbContext context) : BaseHandler(context), IRequestHandler<UpdateUserCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<UserMapping>());
        var mapper = new Mapper(config);
        
        try
        {
            var user = await _context.Users.FindAsync(request.id);
            if (user == null)
            {
                throw new NotFoundException(request.id);
            }

            // Map the incoming DTO to the existing user entity
            mapper.Map(request.user, user);

            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseDto(user.Id, "User updated", StatusCodes.Accepted);
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"User can not be updated \n \n {e.Message} \n \n", StatusCodes.BadRequest);
            return response;
        }
    }
}