using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Subjects.Mapping;
using UniVerServer.Subjects.Models;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<CreateSubjectCommand, ResponseDto>
{
    public async Task<ResponseDto> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<SubjectMapping>());
        var mapper = new Mapper(config);
        ResponseDto response;
        try
        {
            bool subjectNameInUse = await _context.Subjects
                .AnyAsync(x => x.Name.Equals(request.subject.Name), cancellationToken);
          

            if (subjectNameInUse)
            {
                response = new ResponseDto(default, "Subject name in use", StatusCodes.Conflict);
                return response;
            }
            request.subject.GenerateSubjectCode();

            bool subjectIdentifierInUse =
                await _context.Subjects.AnyAsync(x => x.Identifier.Equals(request.subject.Identifier),
                    cancellationToken);
            
            if (subjectIdentifierInUse)
            {
                response = new ResponseDto(default, "Subject Code in use", StatusCodes.Conflict);
                return response;
            }
            
            var newSubject = mapper.Map<Subject>(request.subject);
            
            _context.Add(newSubject);
            await _context.SaveChangesAsync(cancellationToken);
            response = new ResponseDto(default, "Subject created", StatusCodes.Accepted);
            return response;
        }
        catch (Exception e)
        {
            response = new ResponseDto(default, $"Could not process subject {e.Message}", StatusCodes.BadRequest);
            return response;
        }
    }
}