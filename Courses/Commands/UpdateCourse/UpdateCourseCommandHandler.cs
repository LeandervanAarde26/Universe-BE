using AutoMapper;
using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Courses.Mapping;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateCourseCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CourseMapping>());
        var mapper = new Mapper(config);
        try
        {
            var course = await _context.Courses.FindAsync(request.id);
            if (course is null)
            {
                response = new ResponseDto(default, $"Can not find coures with id: {request.id}", StatusCodes.NotFound);
                return response;
            }

            mapper.Map(request.course, course);
            await _context.SaveChangesAsync(cancellationToken);
            
            response = new ResponseDto(request.id, "Course updated", StatusCodes.Ok);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Could not update course: {e.Message}", StatusCodes.BadRequest);
            return response;
        }
    }
}