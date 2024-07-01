using MediatR;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Queries.GetById;

public record GetUserByIdQuery(Guid Id) : IRequest<GetUserByIdDto>;