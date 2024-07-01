using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Events.Commands.UpdateDate;

public record UpdateEventDateCommand(Guid id, DateTime date): IRequest<ResponseDto>;