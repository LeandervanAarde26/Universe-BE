using MediatR;
using UniVerServer.Subjects.DTO;

namespace UniVerServer.Subjects.Queries.GetAllSubjects;

public record GetSubjectQuery() : IRequest<IEnumerable<GetSubjectDto>>;