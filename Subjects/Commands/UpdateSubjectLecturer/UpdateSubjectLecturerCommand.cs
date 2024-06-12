using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Subjects.Commands.UpdateSubjectLecturer;

public record UpdateSubjectLecturerCommand(Guid id, Guid lecturerId): IRequest<ResponseDto>;