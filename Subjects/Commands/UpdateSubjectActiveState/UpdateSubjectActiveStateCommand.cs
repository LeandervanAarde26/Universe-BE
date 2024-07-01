using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Subjects.Commands.UpdateSubjectActiveState;

public record UpdateSubjectActiveStateCommand(Guid id): IRequest<ResponseDto>;