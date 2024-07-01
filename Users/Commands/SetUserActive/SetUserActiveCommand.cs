using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Users.Commands.SetUserActive;

public record SetUserActiveCommand(Guid id) : IRequest<ResponseDto>;