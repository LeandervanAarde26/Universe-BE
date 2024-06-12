using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;

namespace UniVerServer.Subjects.Commands.UpdateSubjectActiveState;

public class UpdateSubjectActiveStateCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateSubjectActiveStateCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateSubjectActiveStateCommand request, CancellationToken cancellationToken)
    {
        ResponseDto response;
        try
        {
            var subject = await context.Subjects.FindAsync(request.id);
            if (subject is null)
                throw new NotFoundException("Can not find subject with specified details");
            
            subject.Active = !subject.Active ;
            subject.DateModified = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);

            response = new ResponseDto(request.id, "Subject  updated", UniVerServer.Enums.StatusCodes.Accepted);
            return response;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Could not assign lecturer to subject", UniVerServer.Enums.StatusCodes.BadRequest);
            return response;
        }
    }
}