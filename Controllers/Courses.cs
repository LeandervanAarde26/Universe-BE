using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Courses.Commands.CreateCourse;
using UniVerServer.Courses.Commands.DeleteCourse;
using UniVerServer.Courses.Commands.UpdateAcceptingStudents;
using UniVerServer.Courses.Commands.UpdateActiveCourse;
using UniVerServer.Courses.DTO;
using UniVerServer.Courses.Queries.GetCourseById;
using UniVerServer.Courses.Queries.GetCourses;
using UniVerServer.Subjects.DTO;

namespace UniVerServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Courses(IMediator mediator) : BaseController(mediator)
{
    private HttpResponseService response = new HttpResponseService();

    // CREATE 
    [HttpPost]
    public async Task<ActionResult<ResponseDto>> CreateCourse([FromBody] CreateCourseRequestDto course) =>
        response.HandleResponse(await mediator.Send(new CreateCourseCommand(course.ConvertCourseDto())));

    // READ 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCoursesDto>>> GetCourses() =>
        Ok(await mediator.Send(new GetCoursesQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCoursesDto>> GetCourse(string id) =>
        Ok(await mediator.Send(new GetCourseByIdQuery(Guid.Parse(id))));
    
    // UPDATE 
    
    // Udate starting date 
    // Means to recalculate EndingDate
    [HttpPatch("Active/{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateActiveFlag(string id, bool flag) =>
        response.HandleResponse(await mediator.Send(new UpdateActiveCourseFlagCommand(Guid.Parse(id), flag)));
    
    // Update active flag
    // NOTE: Setting this flag to false will also update the accepting students flag to false.
    [HttpPatch("AcceptStudents/{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateAcceptingStudentsFlag(string id, [FromBody] bool flag) =>
        response.HandleResponse(await mediator.Send(new UpdateAcceptingStudentsCommand(Guid.Parse(id), flag)));
    

    // General update



    //     // PUT: api/StudentCourses/5
    //     // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //     [HttpPut("{id}")]
    //     public async Task<IActionResult> PutStudentCourses(string id, StudentCourses studentCourses)
    //     {
    //         if (id != studentCourses.grade_id)
    //         {
    //             return BadRequest();
    //         }
    //
    //         _context.Entry(studentCourses).State = EntityState.Modified;
    //
    //         try
    //         {
    //             await _context.SaveChangesAsync();
    //         }
    //         catch (DbUpdateConcurrencyException)
    //         {
    //             if (!StudentCoursesExists(id))
    //             {
    //                 return NotFound();
    //             }
    //             else
    //             {
    //                 throw;
    //             }
    //         }
    //
    //         return NoContent();
    //     }

    //DELETE
    [HttpDelete("Purge/{id}")]
    public async Task<ActionResult<ResponseDto>> PurgeCourse(string id) =>
        response.HandleResponse(await mediator.Send(new PurgeCourseCommand(Guid.Parse(id))));
}

/*
 TODO:
 Get courses by lecturer -> id
 Get active courses
 Get courses that are accepting students
 Get courses starting in a certain month
 Get courses ending in a certain month
 Get courses with certain subject type
 */
