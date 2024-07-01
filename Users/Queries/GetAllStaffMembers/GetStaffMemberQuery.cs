using MediatR;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Queries.GetAllStaffMembers;

public record GetStaffMemberQuery(string RoleName): IRequest<IEnumerable<GetStaffMembersDto>>;