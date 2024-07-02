using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Enrollments.Commands.CreateEnrollment;
using UniVerServer.Enrollments.Commands.DeleteEnrollment;
using UniVerServer.Enrollments.Commands.UpdateEnrollmentStatus;
using UniVerServer.Enrollments.Commands.UpdateGrade;
using UniVerServer.Enrollments.Commands.UpdateStatusByStudentId;
using UniVerServer.Enrollments.DTO;
using UniVerServer.Enrollments.Enums;
using UniVerServer.Enrollments.Queries.GetAllEnrollments;
using UniVerServer.Enrollments.Queries.GetEnrollmentByCourseId;

namespace UniVerServer.Controllers;
// TODO: CRON JOB to Delete enrolments that are inactive for more than a year. (modified date && Status check) 
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

          [HttpPatch("Status/{enrolmentId}")]
          public async Task<ActionResult<ResponseDto>> UpdateEnrollmentStatus(string enrolmentId,
               EnrollmentStatus status) =>
               response.HandleResponse(
                    await mediator.Send(new UpdateEnrollmentStatusCommand(Guid.Parse(enrolmentId), status)));

          [HttpPatch("StatusById/{studentId}")]
          public async Task<ActionResult<ResponseDto>> UpdateEnrollmentStatusByStudentId(string studentId,
               UpdateEnrollmentStatusRequestDto data) =>
               response.HandleResponse(
                    await mediator.Send(new UpdateStatusByStudentIdCommand(Guid.Parse(studentId), new UpdateStatusDto{CourseId = Guid.Parse(data.CourseId), Status = data.Status})));

          //DELETE
          [HttpDelete("{id}")]
          public async Task<ActionResult<ResponseDto>> DeleteEnrolment(string id) =>
               response.HandleResponse(await mediator.Send(new DeleteEnrollmentCommand(Guid.Parse(id))));
     }



