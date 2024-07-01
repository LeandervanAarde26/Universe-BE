using AutoMapper;
using UniVerServer.Roles.DTO;

namespace UniVerServer.Roles.Mapping;

public class RolesMapping : Profile
{
   public RolesMapping()
   {
      CreateMap<CreateRoleDto, Models.Roles>();
   }
}