using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Subjects.Commands.CreateSubject;
using UniVerServer.Subjects.Commands.UpdateSubject;
using UniVerServer.Subjects.Commands.UpdateSubjectActiveState;
using UniVerServer.Subjects.Commands.UpdateSubjectLecturer;
using UniVerServer.Subjects.DTO;
using UniVerServer.Subjects.Queries.GetAllSubjects;
using UniVerServer.Subjects.Queries.GetSubject.GetSubjectbyId;
using UniVerServer.Subjects.Queries.GetSubject.GetSubjectByIdentifier;

namespace UniVerServer.Controllers;

//TODO: Integrate auth middleware 
// TODO: Update table to take a modified date in
// TODO: GLOBAL: Audit tables and loggers table/database.
[Route("api/[controller]")]
[ApiController]
public class Subjects(IMediator mediator) : BaseController(mediator)
{
    private HttpResponseService response = new HttpResponseService();
    // CREATE 
    [HttpPost("")]
    public async Task<ActionResult<ResponseDto>> CreateSubject([FromBody] CreateSubjectDto subject) =>
        response.HandleResponse(await mediator.Send(new CreateSubjectCommand(subject)));
    // READ 
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<GetSubjectDto>>> ReadSubjects() =>
        Ok(await mediator.Send(new GetSubjectQuery()));
    // Get Subject by Id
    [HttpGet("{id}")]
    public async Task<ActionResult<GetSingleSubjectDto>> ReadSubject(string id) =>
        Ok(await mediator.Send(new GetSubjectByIdQuery(Guid.Parse(id))));
    
    // Get Subject by SubjectCode
    [HttpGet("Identifier/{identifier}")]
    public async Task<ActionResult<GetSingleSubjectDto>> ReadSubjectByIdentifier(string identifier) =>
        Ok(await mediator.Send(new GetSubjectByIdentifierQuery(identifier)));

    // UPDATE
    [HttpPatch("UpdateLecturer/{subjectId}")]
    public async Task<ActionResult<ResponseDto>> UpdateLecturer(string subjectId, [FromBody] string lecturerId) =>
        response.HandleResponse(
            await mediator.Send(new UpdateSubjectLecturerCommand(Guid.Parse(subjectId), Guid.Parse(lecturerId))));
    [HttpPatch("Active/{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateSubjectActiveState(string id) =>
        response.HandleResponse(
            await mediator.Send(new UpdateSubjectActiveStateCommand(Guid.Parse(id))));

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateSubject(string id, [FromBody] UpdateSubjectDto subject) =>
        response.HandleResponse(await mediator.Send(new UpdateSubjectCommand(Guid.Parse(id), subject)));
    
    // DELETE
    //TODO:  Need to finish the enrollments so that delete can delete through a transaction.
// DELETE: api/Subjects/5
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteSubjects(int id)
//         {
//             if (_context.Subjects == null)
//             {
//                 return NotFound();
//             }
//             var subjects = await _context.Subjects.FindAsync(id);
//             if (subjects == null)
//             {
//                 return NotFound();
//             }
//
//             _context.Subjects.Remove(subjects);
//             await _context.SaveChangesAsync();
//
//             return NoContent();
//         }
//

}

// TODO: SEPERATE CONTROLLER 
//         [HttpGet("lecturerfees")]
//         public async Task<ActionResult<CollectiveLecturerSalary>> GetAllLecturerFees()
//         {
//             if (_context.Subjects == null)
//             {
//                 return NotFound();
//             }
//      
//             var lecturersPayment = await (from subject in _context.Subjects
//                                      join lecturer in _context.People
//                                      on subject.lecturer_id
//                                      equals lecturer.person_id
//                                      join role in _context.Roles
//                                      on lecturer.role equals role.role_id                      
//                                      select new LecturerPayment
//                                      {
//                                         subject_id = subject.subject_id,
//                                         subject_name = subject.subject_name,
//                                         lecturer_id = lecturer.person_id,
//                                         lecturer = lecturer.first_name + " " + lecturer.last_name,
//                                         subject_class_amount = subject.subject_class_amount,
//                                         course_start = subject.course_start,
//                                         class_time = subject.subject_class_runtiem,
//                                         monthlyIncome = Math.Round((subject.subject_class_amount * (subject.subject_class_runtiem/60)) * (((decimal)subject.course_start.Day / new DateTime(subject.course_start.Year, subject.course_start.Month, DateTime.DaysInMonth(subject.course_start.Year, subject.course_start.Month)).Day) * role.rate), 2),
//                                         hoursWorked = Math.Round((subject.subject_class_amount * (subject.subject_class_runtiem / 60)) * (((decimal)subject.course_start.Day / new DateTime(subject.course_start.Year, subject.course_start.Month, DateTime.DaysInMonth(subject.course_start.Year, subject.course_start.Month)).Day)), 2)
//                                      })
//                                       .ToListAsync();
//
//                 var result = lecturersPayment
//                              .GroupBy(item => item.lecturer)
//                              .Select(group => new CollectiveLecturerSalary
//                              {
//                                 lecturerId = group.ToList()[0].lecturer_id,
//                                 lecturer = group.Key,
//                                 //subjects = group.ToList(),
//                                 totalHoursWorked = group.Sum(item =>
//                                 {
//                                     return item.hoursWorked;
//                                 }),
//                                 monthlyIncome = group.Sum(item =>
//                                 {
//                                     return item.monthlyIncome;
//                                 })
//                             })
//                              .ToList();
//
//             return Ok(result);
//         }