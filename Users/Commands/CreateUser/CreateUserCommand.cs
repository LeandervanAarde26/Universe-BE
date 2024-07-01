using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Commands.CreateUser;

public record CreateUserCommand(CreateUserDto user): IRequest<ResponseDto>;