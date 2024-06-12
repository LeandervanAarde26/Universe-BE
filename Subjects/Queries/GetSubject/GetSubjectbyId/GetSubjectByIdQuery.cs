using MediatR;
using UniVerServer.Subjects.DTO;

namespace UniVerServer.Subjects.Queries.GetSubject.GetSubjectbyId;

public record GetSubjectById(Guid id): IRequest<GetSubjectDto>;
