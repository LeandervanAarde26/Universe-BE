using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.Enums;

namespace UniVerServer.Enrollments.Commands.UpdateStatusByStudentId;

public class UpdateStatusByStudentIdCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateStatusByStudentIdCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateStatusByStudentIdCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(x =>
                x.StudentId.Equals(request.studentId) && x.CourseId.Equals(request.data.CourseId));
            if (enrollment is null)
            {
                response = new ResponseDto(default, $"Enrollment was not found",
                    UniVerServer.Enums.StatusCodes.NotFound);
                return response; 
            }

            enrollment.Status = request.data.Status;
            enrollment.Modified = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(request.studentId, "Student status has been updated", UniVerServer.Enums.StatusCodes.Ok);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Could not update enrollment grade: {e.Message}",
                UniVerServer.Enums.StatusCodes.BadRequest);
            return response;
        }
    }
}