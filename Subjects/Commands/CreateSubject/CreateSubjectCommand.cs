using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Subjects.DTO;

namespace UniVerServer.Subjects.Commands.CreateSubject;

public record CreateSubjectCommand(CreateSubjectDto subject): IRequest<ResponseDto>;