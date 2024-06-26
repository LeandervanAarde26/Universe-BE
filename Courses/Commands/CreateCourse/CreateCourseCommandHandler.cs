using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Courses.Mapping;
using UniVerServer.Courses.Models;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler(ApplicationDbContext context)
    : BaseHandler(context), IRequestHandler<CreateCourseCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CourseMapping>());
        var mapper = new Mapper(config);
        try
        {
            bool existingCourse = await _context.Courses.AnyAsync(x =>
                x.SubjectId.Equals(request.course.SubjectId) && x.StartDate.Equals(request.course.StartDate));
            if (existingCourse)
            {
                response = new ResponseDto(default,
                    $"Course with subject ID {request.course.SubjectId} already exists", StatusCodes.Conflict);
                return response;
            }

            var subject = await _context.Subjects.FindAsync(request.course.SubjectId);
            if (subject is null)
            {
                response = new ResponseDto(default,
                    $"Subject ID {request.course.SubjectId} does not exist ", StatusCodes.NotFound);
                return response; 
            }
            request.course.CalculateEndDate(subject.ClassDayIntervals, subject.ClassRepitions, request.course.StartDate);
            _context.Courses.Add(mapper.Map<Course>(request.course));
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(default, "Course added", StatusCodes.Accepted);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Could not create course", StatusCodes.BadRequest);
            return response;
        }
    }
}