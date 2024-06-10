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
         .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
         .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.Role.HourlyRate))
         .ForMember(dest => dest.CanAccess, opt => opt.MapFrom(src => src.Role.CanAccess))
         .ConstructUsing(src => new GetStaffMembersDto(
            src.DateCreated,
            src.Role.Name,
            src.Role.HourlyRate,
            src.Role.CanAccess
         ));
      CreateMap<Models.Users, GetstudentsDto>()
         .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
         .ConstructUsing(src => new GetstudentsDto(
            src.DateCreated,
            src.Role.Name
         ));
   }
}