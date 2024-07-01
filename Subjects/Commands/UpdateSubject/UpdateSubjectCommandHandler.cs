using AutoMapper;
using MediatR;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using UniVerServer.Subjects.Mapping;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Subjects.Commands.UpdateSubject;

public class UpdateSubjectCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<UpdateSubjectCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<SubjectMapping>());
        var mapper = new Mapper(config);
        ResponseDto response;
        try
        {
            var subjectToUpdate = await _context.Subjects.FindAsync(request.id);
            if (subjectToUpdate is null)
                throw new NotFoundException($"{request.id}: Subject could not be found");
            subjectToUpdate.DateModified = DateTime.UtcNow;
            mapper.Map(request.subject, subjectToUpdate);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseDto(subjectToUpdate.Id, "Subject updated", StatusCodes.Accepted);
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, "Could not update Subject", StatusCodes.BadRequest);
            return response;
        }
    }
}