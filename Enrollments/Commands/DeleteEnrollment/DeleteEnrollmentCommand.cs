using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Enrollments.Commands.DeleteEnrollment;

public record DeleteEnrollmentCommand(Guid id) : IRequest<ResponseDto>;