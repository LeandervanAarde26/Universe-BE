using MediatR;
using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Queries.GetCoursesAcceptingStudents;

public record CoursesAcceptingStudentsQuery(): IRequest<IEnumerable<GetCoursesDto>>;