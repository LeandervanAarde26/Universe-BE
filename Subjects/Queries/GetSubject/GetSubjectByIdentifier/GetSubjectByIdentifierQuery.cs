using MediatR;
using UniVerServer.Subjects.DTO;

namespace UniVerServer.Subjects.Queries.GetSubject.GetSubjectByIdentifier;

public record GetSubjectByIdentifierQuery(string identifier) : IRequest<GetSingleSubjectDto>;

