using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Events.Dto;

namespace UniVerServer.Events.Commands.CreateEvent;

public record CreateEventCommand(CreateEventDto ev): IRequest<ResponseDto>;