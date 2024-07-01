using AutoMapper;
using UniVerServer.Subjects.DTO;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Mapping;

public class UserMapping : Profile
{
   public UserMapping()
   {
      CreateMap<CreateUserDto, Models.Users>();
      CreateMap< Models.Users, CreateUserDto>();
      CreateMap<Models.Users, UpdateUserDto>();
      CreateMap<UpdateUserDto, Models.Users>();
      CreateMap<Models.Users, LecturerInformation>();
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
      
      CreateMap<Models.Users, GetUserByIdDto>()
         .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.Name))
         .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.Role.HourlyRate))
         .ForMember(dest => dest.CanAccess, opt => opt.MapFrom(src => src.Role.CanAccess))
         .ConstructUsing(src => new GetUserByIdDto(
            src.DateCreated,
            src.Role.Name,
            src.Role.HourlyRate,
            src.Role.CanAccess
         ));
   }
}