using MediatR;
using UniVerServer.Abstractions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Enrollments.Commands.UpdateEnrollmentStatus;

public class UpdateEnrollmentStatusCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateEnrollmentStatusCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateEnrollmentStatusCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var enrollment = await _context.Enrollments.FindAsync(request.EnrollmentId);
            if (enrollment is null)
            {
                response = new ResponseDto(default, "Enrollment not found", StatusCodes.NotFound);
                return response;
            }

            enrollment.Status = request.status;
            enrollment.Modified = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(request.EnrollmentId, "Enrollment status has been updated", StatusCodes.Ok);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Could not update status: {e.Message}", StatusCodes.BadRequest);
            return response;
        }
    }
}