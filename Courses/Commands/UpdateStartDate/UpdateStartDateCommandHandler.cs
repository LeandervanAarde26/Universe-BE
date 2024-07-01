using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Courses.Extensions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Courses.Commands.UpdateStartDate;

public class UpdateStartDateCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateStartDateCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateStartDateCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;

        try
        {
            var course = await _context.Courses.FindAsync(request.Id);
            
            if (course is null)
            {
                response = new ResponseDto(default, $"Course with id: {request.Id} does not exist", Enums.StatusCodes.NotFound);
                return response;
            }
            var date = DateTime.UtcNow.ToUniversalTime();
            if (request.newDate < date)
            {
                response = new ResponseDto(default, $"Can not reset course starting date to before current date", Enums.StatusCodes.Forbidden);
                return response;
            }

            var subject = await _context.Subjects.FindAsync(course.SubjectId);
            course.StartDate = request.newDate;
            course.EndDate = request.newDate.CalculateEndDate(subject.ClassDayIntervals, subject.ClassRepitions);
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(default, "Course has been updated", StatusCodes.Accepted);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Could not update date : {e.Message}", Enums.StatusCodes.BadRequest);
            return response;
        }
    }
}