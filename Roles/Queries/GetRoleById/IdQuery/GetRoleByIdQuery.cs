using MediatR;

namespace UniVerServer.Roles.Queries.GetRoleById.IdQuery;

public record GetRoleByIdQuery(Guid id): IRequest<Models.Roles>;