using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Courses.Commands.CreateCourse;
using UniVerServer.Courses.Commands.DeleteCourse;
using UniVerServer.Courses.Commands.UpdateAcceptingStudents;
using UniVerServer.Courses.Commands.UpdateActiveCourse;
using UniVerServer.Courses.Commands.UpdateCourse;
using UniVerServer.Courses.Commands.UpdateStartDate;
using UniVerServer.Courses.DTO;
using UniVerServer.Courses.Queries.GetActiveCourses;
using UniVerServer.Courses.Queries.GetCourseById;
using UniVerServer.Courses.Queries.GetCourseByLecturer;
using UniVerServer.Courses.Queries.GetCourseInMonth.Ending;
using UniVerServer.Courses.Queries.GetCourseInMonth.GetCourseInMonthQuery;
using UniVerServer.Courses.Queries.GetCourses;
using UniVerServer.Courses.Queries.GetCoursesAcceptingStudents;

namespace UniVerServer.Controllers;
/*
 TODO:
    FIX MAPPING!!!
 */

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
    
    // Get active courses
    [HttpGet("ActiveCourses")]
    public async Task<ActionResult<IEnumerable<GetCoursesDto>>> GetActiveCourses() =>
        Ok(await mediator.Send(new GetActiveCoursesQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCoursesDto>> GetCourse(string id) =>
        Ok(await mediator.Send(new GetCourseByIdQuery(Guid.Parse(id))));
    
    // Get courses that are accepting students
    [HttpGet("AcceptingStudents")]
    public async Task<ActionResult<IEnumerable<GetCoursesDto>>> GetAcceptingStudentsCourse() =>
        Ok(await mediator.Send(new CoursesAcceptingStudentsQuery()));
    
    
    //TODO: Find a way to dynamically see if user wants start/end
    // Get courses starting in a certain month
    [HttpGet("CoursesStarting/{date}")]
    public async Task<ActionResult<IEnumerable<GetCoursesDto>>> GetCourseStartingInMonth(string date) =>
        Ok(await mediator.Send(new GetCourseInStartMonthQuery(DateTime.Parse(date).ToUniversalTime())));
    
    // Get courses ending in a certain month
    [HttpGet("CourseEnding/{date}")]
    public async Task<ActionResult<IEnumerable<GetCoursesDto>>> GetCourseEndingMonth(string date) =>
        Ok(await mediator.Send(new GetCourseEndingMonthQuery(DateTime.Parse(date).ToUniversalTime())));
    
    [HttpGet("CourseLecturer/{id}")]
    public async Task<ActionResult<IEnumerable<GetCoursesDto>>> GetCourseByLecturer(string id) =>
        Ok(await mediator.Send(new GetCourseByLecturerQuery(Guid.Parse(id))));
    // UPDATE 
    [HttpPatch("StartingDate/{id}")]
    // Update starting date 
    // Means to recalculate EndingDate
    public async Task<ActionResult<ResponseDto>> UpdateStartingDate(string id, String date) =>
        response.HandleResponse(
            await mediator.Send(new UpdateStartDateCommand(Guid.Parse(id), DateTime.Parse(date).ToUniversalTime())));
    
    // Update active flag
    // NOTE: Setting this flag to false will also update the accepting students flag to false.
    [HttpPatch("AcceptStudents/{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateAcceptingStudentsFlag(string id, [FromBody] bool flag) =>
        response.HandleResponse(await mediator.Send(new UpdateAcceptingStudentsCommand(Guid.Parse(id), flag)));
    
    //General update.
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateCourse(string id, [FromBody] UpdateCourseDto course) =>
        response.HandleResponse(await mediator.Send(new UpdateCourseCommand(Guid.Parse(id), course)));

    //DELETE
    //SOFT DELETE
    [HttpDelete("Active/{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateActiveFlag(string id, bool flag) =>
        response.HandleResponse(await mediator.Send(new UpdateActiveCourseFlagCommand(Guid.Parse(id), flag)));
    
    //PURGE
    [HttpDelete("Purge/{id}")]
    public async Task<ActionResult<ResponseDto>> PurgeCourse(string id) =>
        response.HandleResponse(await mediator.Send(new PurgeCourseCommand(Guid.Parse(id))));
}


