using MediatR;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Abstractions;
using UniVerServer.Courses.DTO;
using UniVerServer.Exceptions;
using UniVerServer.Subjects.DTO;
using UniVerServer.Users.DTO;

namespace UniVerServer.Courses.Queries.GetCourseById;

public class GetCourseByIdQueryHandler(ApplicationDbContext context): BaseHandler(context), IRequestHandler<GetCourseByIdQuery, GetCoursesDto>
{
    public async Task<GetCoursesDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var course = await _context.Courses
                .Include(x => x.Subject)
                .Include(x => x.Subject.Lecturer)
                .Where(x => x.Id.Equals(request.Id))
                .FirstOrDefaultAsync(cancellationToken);

            if (course is null)
            {
                throw new NotFoundException($"Requested Course with Id {request.Id} was not found");
            }

            var formattedCourse = new GetCoursesDto
            {
                Id = course.Id,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Active = course.Active,
                AcceptingStudents = course.AcceptingStudents,
                // Subject information
                Subject = new SubjectInCourseDto
                {
                    SubjectName = course.Subject.Name,
                    SubjectCredits = course.Subject.Credits,
                    SubjectYear = course.Subject.Year,
                    ClassRuntime = course.Subject.ClassRuntime,
                    SubjectIdentifier = course.Subject.Identifier,
                },
                // Lecturer information
                Lecturer = new LecturerInformation
                {
                    LecturerId = course.Subject.Lecturer.Id,
                    FullNames = $"{course.Subject.Lecturer.FirstNames} {course.Subject.Lecturer.LastNames}",
                    Email = course.Subject.Lecturer.IssuedEmail,
                    Active = course.Subject.Lecturer.Active,
                    ProfileImage = course.Subject.Lecturer.ProfileImage
                },

                DateCreated = course.DateCreated
            };
            return formattedCourse;
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