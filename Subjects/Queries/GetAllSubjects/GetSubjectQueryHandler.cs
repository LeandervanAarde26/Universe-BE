using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Subjects.DTO;
using UniVerServer.Subjects.Mapping;

namespace UniVerServer.Subjects.Queries.GetAllSubjects;

public class GetSubjectQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetSubjectQuery, IEnumerable<GetSubjectDto>>
{
    public async Task<IEnumerable<GetSubjectDto>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<SubjectMapping>());
        var mapper = new Mapper(config);
        try
        {
            // TODO: ADD mpping for this.
            var subjects = await _context.Subjects
                .Include(x => x.Lecturer)
                .Select(x => new GetSubjectDto
                {
                    Id = x.Id,
                    Identifier = x.Identifier,
                    Name = x.Name,
                    Description = x.Description,
                    Cost = x.Cost,
                    Credits = x.Credits,
                    LecturerName = x.Lecturer.FirstNames + " " + x.Lecturer.LastNames,
                    LecturerId = x.LecturerId,
                    ColorHex = x.ColorHex,
                    Image = x.Image,
                    Active = x.Active,
                    ClassRuntime = x.ClassRuntime,
                    ClassRepitions = x.ClassRepitions,
                    Type = x.Type.ToString(),
                    Year = x.Year,
                    DateCreated = x.DateCreated.ToShortDateString()
                })
                .ToListAsync(cancellationToken);

            return subjects;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}