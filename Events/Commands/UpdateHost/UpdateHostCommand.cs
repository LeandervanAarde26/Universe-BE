using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Events.Dto;

namespace UniVerServer.Events.Commands.UpdateHost;

public record UpdateHostCommand(UpdateEventHostDto data): IRequest<ResponseDto>;