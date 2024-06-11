using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Commands.UpdatePhoneNumber;

public record UpdateUserPhoneNumberCommand(UpdatePhoneNumberDto fields) : IRequest<ResponseDto>;
