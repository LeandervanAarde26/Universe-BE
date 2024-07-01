using MediatR;
using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Queries.GetCourseInMonth.GetCourseInMonthQuery;

public record GetCourseInStartMonthQuery(DateTime startDate): IRequest<IEnumerable<GetCoursesDto>>;