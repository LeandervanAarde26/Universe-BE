using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Roles.Commands.UpdateRoleRate;

public record UpdateRoleRateCommand(Guid id, decimal newRate): IRequest<ResponseDto>;