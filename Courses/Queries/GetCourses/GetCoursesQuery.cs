using MediatR;
using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Queries.GetCourses;

public record GetCoursesQuery() : IRequest<IEnumerable<GetCoursesDto>>;