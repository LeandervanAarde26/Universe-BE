using MediatR;

namespace UniVerServer.Roles.Queries.GetAllRoles;


public record GetRolesQuery(): IRequest<IEnumerable<Models.Roles>>;