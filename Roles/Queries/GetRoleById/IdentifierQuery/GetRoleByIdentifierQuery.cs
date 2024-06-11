using MediatR;

namespace UniVerServer.Roles.Queries.GetRoleById.IdentifierQuery;

public record GetRoleByIdentifierQuery(string identifier) : IRequest<Models.Roles>;

