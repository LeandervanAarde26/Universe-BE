using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Commands.CreateCourse;

public record CreateCourseCommand(CreateCourseDto course): IRequest<ResponseDto>;