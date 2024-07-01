using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Events.Dto;

namespace UniVerServer.Events.Commands.UpdateEvent;

public record UpdateEventCommand(UpdateEventDto ev, Guid eventId) : IRequest<ResponseDto>;