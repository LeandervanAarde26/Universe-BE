using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.DTO;

namespace UniVerServer.Enrollments.Commands.CreateEnrollment;

public record CreateEnrollmentCommand(CreateEnrollmentDto enrollment): IRequest<ResponseDto>;