using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Courses.Commands.UpdateStartDate;

public record UpdateStartDateCommand(Guid Id, DateTime newDate): IRequest<ResponseDto>;