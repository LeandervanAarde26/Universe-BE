using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Events.Commands.DeleteEvent;

public record DeleteEventCommand(Guid id): IRequest<ResponseDto>;