using MediatR;
using UniVerServer.Abstractions;

namespace UniVerServer.Enrollments.Commands.DeleteEnrollment;

public class DeleteEnrollmentCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<DeleteEnrollmentCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var enrollment = await _context.Enrollments.FindAsync(request.id);
            if (enrollment is null)
            {
                response = new ResponseDto(default, "Enrollment not found", UniVerServer.Enums.StatusCodes.NotFound);
                return response;
            }

            enrollment.ActiveEnrollment = false;
            enrollment.Modified = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(request.id, "Enrollment status has been updated", UniVerServer.Enums.StatusCodes.Ok);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Could not update status: {e.Message}", UniVerServer.Enums.StatusCodes.BadRequest);
            return response;
        }
    }
}