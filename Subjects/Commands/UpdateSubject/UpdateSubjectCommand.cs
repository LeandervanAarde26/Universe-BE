using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Subjects.DTO;

namespace UniVerServer.Subjects.Commands.UpdateSubject;

public record UpdateSubjectCommand(Guid id, UpdateSubjectDto subject): IRequest<ResponseDto>;