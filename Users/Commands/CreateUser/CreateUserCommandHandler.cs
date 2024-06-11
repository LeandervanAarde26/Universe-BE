using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Extensions;
using UniVerServer.Models;
using UniVerServer.Users.DTO;
using UniVerServer.Users.Mapping;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Users.Commands.CreateUser;

public class CreateUserCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<CreateUserCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        //TODO: Optimise and improve this autism.
        //TODO: Luhn algorithm to be inserted FOR ID
        //TODO: Cellphone number validator to be inserted
        ResponseDto response;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<UserMapping>());
        var mapper = new Mapper(config);
        try
        {
            // Check if email exists on the database;
            if (!request.user.PersonalEmail.IsValidEmail())
            {
                response = new ResponseDto(default, $"Cannot process email {request.user.PersonalEmail}",
                    StatusCodes.Unprocessable);
                return response;
            }
            bool emailExists = await context.Users.AnyAsync(u => u.PersonalEmail.Equals(request.user.PersonalEmail) || u.IssuedEmail.Equals(request.user.PersonalEmail), cancellationToken);
            if (emailExists)
            {
                response = new ResponseDto(default, "User already exists", StatusCodes.Conflict);
                return response;
            }

            Roles.Models.Roles foundRole = await _context.Roles.FirstOrDefaultAsync(x => x.Identifier.Equals(request.user.RoleId));
            if (foundRole == null)
            {
                response = new ResponseDto(default, $"{request.user.Identifier}, not found", StatusCodes.NotFound);
                return response;
            }
            
            int requiredCredits;

            if (foundRole.Identifier == "R1")
            {
                requiredCredits = 120 * 3;
            } else if (foundRole.Identifier == "R2")
            {
                requiredCredits = 120;
            }
            else
            {
                requiredCredits = 0;
            }
            
            var newUser = new CreateUserDto(
                request.user.FirstNames,
                request.user.LastNames,
                request.user.IdentityNumber,
                request.user.PersonalEmail,
                request.user.ContactNumber,
                foundRole.Id.ToString()
            );
            newUser.SetRequiredCredits(requiredCredits);
            
            _context.Users.Add(mapper.Map<Models.Users>(newUser));
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(default, "User Added", StatusCodes.Accepted);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Cannot create user \n \n" + e.Message, StatusCodes.BadRequest);
            return response;
        }
    }
}