using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Courses.Models;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Courses.Commands.DeleteCourse;

public class PurgeCourseCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<PurgeCourseCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(PurgeCourseCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            Course course = await _context.Courses.FindAsync(request.id);
            if (course is null)
            {
                response = new ResponseDto(default, $"Course with id: {request.id} does not exist", StatusCodes.NotFound);
                return response;
            }
            
            context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(default, "Course has been removed", StatusCodes.Ok);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}