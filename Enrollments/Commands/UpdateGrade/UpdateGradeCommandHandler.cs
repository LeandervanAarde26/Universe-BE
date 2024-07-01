using AutoMapper;
using Azure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.Enums;
using UniVerServer.Enrollments.Mapping;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Enrollments.Commands.UpdateGrade;

public class UpdateGradeCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateGradeCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        var config = new MapperConfiguration(cfg => cfg.AddProfile<EnrollmentMapping>());
        var mapper = new Mapper(config);
        try
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(x =>
                x.StudentId.Equals(request.data.StudentId) && x.CourseId.Equals(request.CourseId));
            if (enrollment is null)
            {
                response = new ResponseDto(default, $"Enrollment was not found", StatusCodes.NotFound);
                return response; 
            }

            GradeType gradeType;

            if (request.data.grade < 48)
            {
                gradeType = GradeType.FAIL;
            } else if (request.data.grade > 75)
            {
                gradeType = GradeType.DISTINCTION;
            }
            else
            {
                gradeType = GradeType.PASS;
            }

            enrollment.Grade = request.data.grade;
            enrollment.GradeType = gradeType;
            enrollment.Modified = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            
            response = new ResponseDto(request.data.StudentId, "Student grade has been updated", StatusCodes.Ok);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Could not update enrollment grade: {e.Message}",
                StatusCodes.BadRequest);
            return response;
        }
    }
}