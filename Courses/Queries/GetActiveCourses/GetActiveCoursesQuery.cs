using MediatR;
using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Queries.GetActiveCourses;

public record GetActiveCoursesQuery() : IRequest<IEnumerable<GetCoursesDto>>;