using MediatR;
using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Queries.GetCourseById;

public record GetCourseByIdQuery(Guid Id): IRequest<GetCoursesDto>;