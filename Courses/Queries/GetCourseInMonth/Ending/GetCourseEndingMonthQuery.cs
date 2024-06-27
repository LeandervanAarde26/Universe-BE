using MediatR;
using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Queries.GetCourseInMonth.Ending;

public record GetCourseEndingMonthQuery(DateTime endDate): IRequest<IEnumerable<GetCoursesDto>>;