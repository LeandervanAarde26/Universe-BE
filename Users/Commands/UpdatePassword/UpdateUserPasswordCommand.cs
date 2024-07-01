using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Users.Commands.UpdatePassword;

public record UpdateUserPasswordCommand(Guid id, string password) : IRequest<ResponseDto>;