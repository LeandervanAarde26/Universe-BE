using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Courses.Commands.UpdateActiveCourse;

public class UpdateActiveCourseFlagCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateActiveCourseFlagCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateActiveCourseFlagCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var course = await _context.Courses.FindAsync(request.id);
            if (course is null)
            {
                response = new ResponseDto(default, $"Course with id: {request.id} does not exist", Enums.StatusCodes.NotFound);
                return response;
            }
            // Check if the course is running
            var date = DateTime.UtcNow.ToUniversalTime();

            if (date > course.StartDate && date < course.EndDate)
            {
                response = new ResponseDto(default, "Course can not change active flag while running",
                    Enums.StatusCodes.Forbidden);
                return response;
            }
            
            if (course.Active.Equals(request.flag))
            {
                string message = request.flag
                    ? "Course is already active"
                    : "Course is already not active";
                response = new ResponseDto(default, message, Enums.StatusCodes.Conflict);
                return response;    
            }
            course.Active = request.flag;
            if (request.flag.Equals(false))
            {
                course.AcceptingStudents = request.flag;
            }
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(default, "Flag has been updated", Enums.StatusCodes.Ok);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Could not update flag : {e.Message}", Enums.StatusCodes.BadRequest);
            return response;
        }
    }
}