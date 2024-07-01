using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Courses.Commands.DeleteCourse;

public record PurgeCourseCommand(Guid id): IRequest<ResponseDto>;