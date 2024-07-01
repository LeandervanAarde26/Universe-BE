using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using UniVerServer.Roles.DTO;
using UniVerServer.Roles.Mapping;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<CreateRoleCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<RolesMapping>());
        var mapper = new Mapper(config);
        try
        {
            // Get the last identifier.
            var lastIndexedRole = (await _context.Roles.ToListAsync(cancellationToken))
                .OrderBy(x => int.Parse(x.Identifier.Substring(1)))
                .LastOrDefault();
            if (lastIndexedRole is null)
                throw new NotFoundException("Can not process the roles in the database, contact support");

            int len = lastIndexedRole.Identifier.Length;
            int startIndex = 1;
            int numericPartLength = len - startIndex;
            int lastIndexOfRoleIdentifier;
            bool canParse = int.TryParse(lastIndexedRole.Identifier.Substring(startIndex, numericPartLength), out lastIndexOfRoleIdentifier);
            
            if (!canParse)
            {
                throw new Exception("Cannot process Identifier, please check data");
            }

            var newRole = new CreateRoleDto(request.Role.Name, request.Role.CanAccess,
                request.Role.PaidRole, request.Role.HourlyRate, lastIndexOfRoleIdentifier);
            
            var roleMapping = mapper.Map<Models.Roles>(newRole);
            context.Roles.Add(roleMapping);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseDto(default, "New role has been created", StatusCodes.Accepted);
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Can not complete operation due to {e.Message}",
                StatusCodes.BadRequest);
            return response;
        }
    }
}