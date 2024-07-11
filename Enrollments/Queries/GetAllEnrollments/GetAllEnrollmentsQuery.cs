using MediatR;
using UniVerServer.Enrollments.DTO;

namespace UniVerServer.Enrollments.Queries.GetAllEnrollments;

public record GetAllEnrollmentsQuery() : IRequest<IEnumerable<GetEnrollmentsDto>>;