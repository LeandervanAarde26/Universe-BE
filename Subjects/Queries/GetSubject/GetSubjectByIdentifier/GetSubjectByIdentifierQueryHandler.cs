using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Exceptions;
using UniVerServer.Subjects.DTO;
using UniVerServer.Subjects.Mapping;

namespace UniVerServer.Subjects.Queries.GetSubject.GetSubjectByIdentifier;

public class GetSubjectByIdentifierQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetSubjectByIdentifierQuery, GetSingleSubjectDto>
{
    public async Task<GetSingleSubjectDto> Handle(GetSubjectByIdentifierQuery request, CancellationToken cancellationToken)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<SubjectMapping>());
        var mapper = new Mapper(config);
        try
        {
            var subject = await _context.Subjects
                .Include(x => x.Lecturer)
                .Where(y => y.Identifier.Equals(request.identifier))
                .FirstOrDefaultAsync(cancellationToken);
            if (subject is null)
                throw new NotFoundException($"Coould not find subject with Id : {request.identifier}");
            var mappedSubjects = mapper.Map<GetSingleSubjectDto>(subject);

            return mappedSubjects;
        }
        catch (NotFoundException e)
        {
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }   
    }
}