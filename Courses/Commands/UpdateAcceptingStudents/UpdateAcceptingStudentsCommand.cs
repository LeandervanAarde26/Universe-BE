using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Courses.Commands.UpdateAcceptingStudents;

public record UpdateAcceptingStudentsCommand(Guid id, bool flag): IRequest<ResponseDto>;