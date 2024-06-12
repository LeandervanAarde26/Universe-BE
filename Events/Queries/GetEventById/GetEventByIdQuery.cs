using MediatR;
using UniVerServer.Events.Dto;

namespace UniVerServer.Events.Queries.GetEventById;

public record GetEventByIdQuery(Guid id): IRequest<GetSingleEventDto>;
