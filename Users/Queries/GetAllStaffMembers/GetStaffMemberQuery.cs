using MediatR;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Queries.GetAllStaffMembers;

public record GetStaffMemberQuery(): IRequest<IEnumerable<GetStaffMembersDto>>;
