using MediatR;
using UniVerServer.Abstractions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Courses.Commands.UpdateAcceptingStudents;

public class UpdateAcceptingStudentsCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateAcceptingStudentsCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateAcceptingStudentsCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var course = await _context.Courses.FindAsync(request.id);
            if (course is null)
            {
                response = new ResponseDto(default, $"Course with id: {request.id} does not exist", StatusCodes.NotFound);
                return response;
            }

            if (course.AcceptingStudents.Equals(request.flag))
            {
                string message = request.flag
                    ? "Course already accepting students"
                    : "Course already not accepting students";
                response = new ResponseDto(default, message, StatusCodes.Conflict);
                return response;    
            }
            course.AcceptingStudents = request.flag;
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(default, "Flag has been updated", StatusCodes.Ok);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Could not update flag : {e.Message}", StatusCodes.BadRequest);
            return response;
        }
    }
}