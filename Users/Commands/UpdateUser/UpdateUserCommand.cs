using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid id, UpdateUserDto user) : IRequest<ResponseDto>;