using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Roles.DTO;

namespace UniVerServer.Roles.Commands.CreateRole;

public record CreateRoleCommand(CreateRoleDto Role) : IRequest<ResponseDto>;