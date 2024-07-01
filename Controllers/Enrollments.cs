using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.Commands.CreateEnrollment;
using UniVerServer.Enrollments.Commands.UpdateGrade;
using UniVerServer.Enrollments.DTO;
using UniVerServer.Enrollments.Queries.GetAllEnrollments;
using UniVerServer.Enrollments.Queries.GetEnrollmentByCourseId;

namespace UniVerServer.Controllers;

     [Route("api/[controller]")]
     [ApiController]
     public class CourseEnrollmentsController(IMediator mediator) : BaseController(mediator)
     {
          private HttpResponseService response = new HttpResponseService();
          
          // CREATE
          [HttpPost]
          public async Task<ActionResult<ResponseDto>>
               CreateEnrolment([FromBody] CreateEnrollmentRequestDto enrollment) =>
               response.HandleResponse(await Mediator.Send(new CreateEnrollmentCommand(new CreateEnrollmentDto()
               {
                    CourseId = Guid.Parse(enrollment.CourseId),
                    StudentId = Guid.Parse(enrollment.StudentId),
                    Grade = enrollment.Grade,
                    GradeType = enrollment.GradeType,
                    Status = enrollment.Status
               })));
          
          //READ
          [HttpGet]
          public async Task<ActionResult<IEnumerable<GetEnrollmentsDto>>> GetAllEnrollments() =>
              Ok(await mediator.Send(new GetAllEnrollmentsQuery()));
          

          [HttpGet("{id}")]
          public async Task<ActionResult<GetEnrollmentsDto>> GetSingleCourseEnrollments(string id) =>
              Ok(await mediator.Send(new GetEnrollmentByCourseIdQuery(Guid.Parse(id))));
          
          
          //UPDATE
          [HttpPatch("{courseId}")]
          public async Task<ActionResult<ResponseDto>> UpdateStudentEnrollmentGrade(string courseId,
               [FromBody] UpdateEnrollmentGradeRequsetDto data) =>
               response.HandleResponse(await mediator.Send(new UpdateGradeCommand(Guid.Parse(courseId),
                    new UpdateEnrollmentGradeDto { grade = data.grade, StudentId = Guid.Parse(data.StudentId) })));
          



          //DELETE
//         // DELETE: api/CourseEnrollments/5
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteCourseEnrollments(int id)
//         {
//             if (_context.Courses == null)
//             {
//                 return NotFound();
//             }
//             var courseEnrollments = await _context.Courses.FindAsync(id);
//             //var person = await _context.People.Where(p => p.person_system_identifier.Equals(id)).FirstOrDefaultAsync();
//
//             var studentToBeRemoved = await _context.Courses.Where(p => p.enrollment_id.Equals(id)).FirstOrDefaultAsync();
//
//
//             if (courseEnrollments == null || studentToBeRemoved == null)
//             {
//                 return NotFound();
//             }
//
//             _context.Courses.Remove(studentToBeRemoved);
//             await _context.SaveChangesAsync();
//
//             return NoContent();
//         }
          //REMOVE ENROLLMENT


     }



//TODO: SEPERATE CONTROLLER ENDPOINTS 

//         [HttpGet("studentFees")]
//         public async Task<ActionResult<CourseEnrollments>> GetAllStudentFees()
//         {
//             if (_context.Courses == null)
//             {
//                 return NotFound();
//             }
//             var data = await (from enrollment in _context.Courses
//                               join learner in _context.People
//                               on enrollment.student_id equals learner.person_system_identifier
//                               join subject in _context.Subjects
//                               on enrollment.Subjects equals subject.subject_id
//                               join lecturer in _context.People
//                               on subject.lecturer_id equals lecturer.person_id
//                               join role in _context.Roles
//                               on lecturer.role equals role.role_id
//                               select new CourseEnrollmentView
//                               {
//                                 
//                                   student_id = learner.person_id,
//                                   student_name = learner.first_name + " " + learner.last_name,
//                                   student_systemIdentifier = learner.person_system_identifier,
//                                   student_number = learner.person_cell,
//                                   student_email = learner.person_email,
//                                   student_credits = learner.person_credits,
//                                   student_needed_credits = learner.needed_credits,
//                                   subject_description = subject.subject_description,
//
//                                   subject_id = subject.subject_id,
//                                   subject_name = subject.subject_name,
//                                   subject_cost = subject.subject_cost,
//                                   subject_credits = subject.subject_credits,
//                                   subject_runtime = subject.subject_class_runtiem,
//                               
//                               }
//                          ).ToListAsync();
//
//             var result = data
//                             .GroupBy(item => item.student_name)
//                             .Select(group => new StudentPayment
//                             {
//
//                                 student = group.Key,
//                                 studentId = group.ToList()[0].student_id,
//                                 studentNumber = group.ToList()[0].student_systemIdentifier,
//                                 credits = group.ToList()[0].student_credits,
//                                 //courses = group.ToList(),
//                                 studentYearlyFee = group.Sum(item =>
//                                 {
//                                     return item.subject_cost;
//                                 }),
//                                 studentMonthlyFee = group.Sum(item =>
//                                 {
//                                     return item.subject_cost;
//                                 }
//                                 ) / 12 ,
//                                 aquiredCredits = group.Sum(item =>
//                                 {
//                                     return item.student_credits;
//                                 })
//                             });
//             return Ok(result);
//         }
