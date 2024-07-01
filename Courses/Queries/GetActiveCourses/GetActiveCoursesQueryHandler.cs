using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Courses.DTO;
using UniVerServer.Subjects.DTO;
using UniVerServer.Users.DTO;

namespace UniVerServer.Courses.Queries.GetActiveCourses;

public class GetActiveCoursesQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetActiveCoursesQuery, IEnumerable<GetCoursesDto>>
{
    public async Task<IEnumerable<GetCoursesDto>> Handle(GetActiveCoursesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var courses = await _context.Courses
                .Select(x => new GetCoursesDto
                {
                    Id = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Active = x.Active,
                    AcceptingStudents = x.AcceptingStudents,
                    // Subject information
                    Subject = new SubjectInCourseDto()
                    {
                        SubjectName = x.Subject.Name,
                        SubjectCredits = x.Subject.Credits,
                        SubjectYear = x.Subject.Year,
                        ClassRuntime = x.Subject.ClassRuntime,
                        SubjectIdentifier = x.Subject.Identifier,
                    },
                    // Lecturer information
                    Lecturer = new LecturerInformation()
                    {
                        LecturerId = x.Subject.Lecturer.Id,
                        FullNames = $"{x.Subject.Lecturer.FirstNames} {x.Subject.Lecturer.LastNames}",
                        Email = x.Subject.Lecturer.IssuedEmail,
                        Active = x.Subject.Lecturer.Active,
                        ProfileImage = x.Subject.Lecturer.ProfileImage
                    },

                    DateCreated = x.DateCreated
                })
                .Where(x => x.Active.Equals(true))
                .ToListAsync(cancellationToken);
            
            return courses;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}