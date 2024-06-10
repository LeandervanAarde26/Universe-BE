using AutoMapper;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Mapping;

public class UserMapping : Profile
{
   public UserMapping()
   {
      CreateMap<CreateUserDto, Models.Users>();
      CreateMap< Models.Users, CreateUserDto>();
   }
}