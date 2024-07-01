using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id): IRequest<ResponseDto>;