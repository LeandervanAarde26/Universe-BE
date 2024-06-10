using AutoMapper;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Mapping;

public class UserMapping : Profile
{
   public UserMapping()
   {
      CreateMap<CreateUserDto, Models.Users>();
      CreateMap< Models.Users, CreateUserDto>();
      CreateMap<Models.Users, GetStaffMembersDto>()
         .ConstructUsing(src => new GetStaffMembersDto(
            src.DateCreated,
            src.role.Name,
            src.role.HourlyRate,
            src.role.CanAccess
         ));
   }
}