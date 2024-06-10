using System.Collections;
using MediatR;
using UniVerServer.Users.DTO;

namespace UniVerServer.Users.Queries.GetAllStudents;

public record GetStudentsQuery(string RoleName) : IRequest<IEnumerable<GetstudentsDto>>;
