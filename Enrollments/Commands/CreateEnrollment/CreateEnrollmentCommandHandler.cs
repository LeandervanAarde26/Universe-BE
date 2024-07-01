using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.Mapping;
using UniVerServer.Enrollments.Models;
using UniVerServer.Exceptions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Enrollments.Commands.CreateEnrollment;

public class CreateEnrollmentCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<CreateEnrollmentCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<EnrollmentMapping>());
        var mapper = new Mapper(config);
        try
        {
            bool existingCourse = await _context.Enrollments.AnyAsync(x => x.CourseId.Equals(request.enrollment.CourseId), cancellationToken);
            if (existingCourse)
            {
                response = new ResponseDto(default, $"Cannot re-add course", StatusCodes.Conflict);
                return response;   
            }

            var student = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.enrollment.StudentId));

            if (student is null)
            {
                throw new NotFoundException(request.enrollment.StudentId);  
            }

            var newEnrollment = mapper.Map<Enrollment>(request.enrollment);
            _context.Add(newEnrollment);
           await _context.SaveChangesAsync(cancellationToken);
           response = new ResponseDto(default, $"Students {request.enrollment.StudentId} added to course",
               StatusCodes.Accepted);
           return response;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Cannot process new enrollment: {e.Message}", StatusCodes.BadRequest);
            return response;
        }
    }
}