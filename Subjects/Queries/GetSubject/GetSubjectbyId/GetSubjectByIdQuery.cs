using MediatR;
using UniVerServer.Subjects.DTO;

namespace UniVerServer.Subjects.Queries.GetSubject.GetSubjectbyId;

public record GetSubjectByIdQuery(Guid id): IRequest<GetSingleSubjectDto>;
