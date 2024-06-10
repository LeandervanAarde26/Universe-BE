using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Users.Commands.PurgeUser;

public record PurgeUserCommand(Guid Id): IRequest<ResponseDto>;
