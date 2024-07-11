using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.DTO;

namespace UniVerServer.Enrollments.Commands.UpdateGrade;

public record UpdateGradeCommand(Guid CourseId, UpdateEnrollmentGradeDto data): IRequest<ResponseDto>;