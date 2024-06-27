using Azure;
using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Commands.UpdateCourse;

public record UpdateCourseCommand(Guid id, UpdateCourseDto course): IRequest<ResponseDto>;