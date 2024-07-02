using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.Enums;

namespace UniVerServer.Enrollments.Commands.UpdateEnrollmentStatus;

public record UpdateEnrollmentStatusCommand(Guid EnrollmentId, EnrollmentStatus status): IRequest<ResponseDto>;