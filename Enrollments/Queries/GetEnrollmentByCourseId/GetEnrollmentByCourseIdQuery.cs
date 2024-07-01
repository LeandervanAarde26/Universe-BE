using MediatR;
using UniVerServer.Enrollments.DTO;

namespace UniVerServer.Enrollments.Queries.GetEnrollmentByCourseId;

public record GetEnrollmentByCourseIdQuery(Guid id): IRequest<GetEnrollmentsDto>;