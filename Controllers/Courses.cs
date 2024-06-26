using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Courses.Commands.CreateCourse;
using UniVerServer.Courses.DTO;
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
    public async Task<ActionResult<IEnumerable<GetSubjectDto>>> GetCourses() =>
        Ok(await mediator.Send(new GetCoursesQuery()));
    
    //     // GET: api/StudentCourses
    //     [HttpGet]
    //     public async Task<ActionResult<IEnumerable<StudentCourses>>> GetStudentCourses()
    //     {
    //       if (_context.StudentCourses == null)
    //       {
    //           return NotFound();
    //       }
    //         return await _context.StudentCourses.ToListAsync();
    //     }

    //     // GET: api/StudentCourses/5
    //     [HttpGet("{id}")]
    //     public async Task<ActionResult<StudentCourses>> GetStudentCourses(string id)
    //     {
    //       if (_context.StudentCourses == null)
    //       {
    //           return NotFound();
    //       }
    //         var studentCourses = await _context.StudentCourses.FindAsync(id);
    //
    //         if (studentCourses == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         return studentCourses;
    //     }

    // UPDATE 

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

    //     // DELETE: api/StudentCourses/5
    //     [HttpDelete("{id}")]
    //     public async Task<IActionResult> DeleteStudentCourses(string id)
    //     {
    //         if (_context.StudentCourses == null)
    //         {
    //             return NotFound();
    //         }
    //         var studentCourses = await _context.StudentCourses.FindAsync(id);
    //         if (studentCourses == null)
    //         {
    //             return NotFound();
    //         }
    //
    //         _context.StudentCourses.Remove(studentCourses);
    //         await _context.SaveChangesAsync();
    //
    //         return NoContent();
    //     }
}
