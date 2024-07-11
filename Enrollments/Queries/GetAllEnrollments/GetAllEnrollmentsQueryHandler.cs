using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Courses.DTO;
using UniVerServer.Enrollments.DTO;
using UniVerServer.Subjects.DTO;
using UniVerServer.Users.DTO;

namespace UniVerServer.Enrollments.Queries.GetAllEnrollments;

public class GetAllEnrollmentsQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetAllEnrollmentsQuery, IEnumerable<GetEnrollmentsDto>>
{
    public async Task<IEnumerable<GetEnrollmentsDto>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Get all courses 
            var courses = await _context.Enrollments
                .Include(x => x.Course)
                .ThenInclude(x => x.Subject)
                .Include(x => x.Student)
                .AsSplitQuery()
                .ToListAsync(cancellationToken);

            var formattedEnrollments = courses
                .GroupBy(e => e.Course)
                .Select(
                    x => new GetEnrollmentsDto
                    {
                        Subject = new SubjectInCourseDto
                        {
                            Id = x.Key.Subject.Id,
                            ClassRuntime = x.Key.Subject.ClassRuntime,
                            SubjectCredits = x.Key.Subject.Credits,
                            SubjectIdentifier = x.Key.Subject.Identifier,
                            SubjectName = x.Key.Subject.Name,
                            SubjectYear = x.Key.Subject.Year

                        },
                        Course = new GetCourseEnrollmentsDto
                        {
                            Id = x.Key.Id,
                            Active = x.Key.Active,
                            StartDate = x.Key.StartDate,
                            EndDate = x.Key.EndDate
                        },
                        Students = x.Select(e => new StudentEnrollmentDto
                            {

                                Name = $"{e.Student.FirstNames} {e.Student.LastNames}",
                                Email = e.Student.IssuedEmail,
                                Identifier = e.Student.Identifier
                            })
                            .ToList()
                    }
                ).ToList();

            return formattedEnrollments;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}