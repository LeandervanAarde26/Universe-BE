using MediatR;
using UniVerServer.Events.Dto;

namespace UniVerServer.Events.Queries.GetEvents;

public record GetEventsQuery() : IRequest<IEnumerable<GetEventsDto>>;