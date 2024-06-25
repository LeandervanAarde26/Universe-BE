using MediatR;
using UniVerServer.Events.Dto;

namespace UniVerServer.Events.Queries.GetEventsInMonth;

public record GetEventsInMonthQuery(DateTime date) : IRequest<GetEventsDto>;