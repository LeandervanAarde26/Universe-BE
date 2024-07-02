using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.DTO;

namespace UniVerServer.Enrollments.Commands.UpdateStatusByStudentId;

public record UpdateStatusByStudentIdCommand(Guid studentId, UpdateStatusDto data) : IRequest<ResponseDto>;