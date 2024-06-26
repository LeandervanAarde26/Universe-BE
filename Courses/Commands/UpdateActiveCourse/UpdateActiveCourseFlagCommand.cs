using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Courses.Commands.UpdateActiveCourse;

public record UpdateActiveCourseFlagCommand(Guid id, bool flag) : IRequest<ResponseDto>;