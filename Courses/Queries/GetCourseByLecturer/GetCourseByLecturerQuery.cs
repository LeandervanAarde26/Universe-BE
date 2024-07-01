using System.Collections;
using MediatR;
using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Queries.GetCourseByLecturer;

public record GetCourseByLecturerQuery(Guid id): IRequest<IEnumerable<GetCoursesDto>>;