using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Roles.Commands.PurgeRole;

public record PurgeRoleCommand(Guid id): IRequest<ResponseDto>;