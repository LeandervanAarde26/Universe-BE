using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using UniVerServer.Subjects.Exceptions;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Subjects.Commands.UpdateSubjectLecturer;

public class UpdateSubjectLecturerCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateSubjectLecturerCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateSubjectLecturerCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var subject = await context.Subjects.FindAsync(request.id);
            if (subject is null)
                throw new NotFoundException("Can not find subject with specified details");
            bool lecturerExists =
                await _context.Users.AnyAsync(x => x.Id.Equals(request.lecturerId) && x.Role.Name.Equals("Lecturer"),
                    cancellationToken);
            if (!lecturerExists)
                throw new NotFoundException(request.lecturerId);

            if (subject.LecturerId.Equals(request.lecturerId))
                throw new LecturerIdMismatchException("Cannot re-assign the same lecturer");

            subject.LecturerId = request.lecturerId;
            subject.DateModified = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);

            response = new ResponseDto(request.id, "Subject lecturer updated", StatusCodes.Accepted);
            return response;
        }
        catch (LecturerIdMismatchException e)
        {
            throw;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Could not assign lecturer to subject", StatusCodes.BadRequest);
            return response;
        }
    }
}