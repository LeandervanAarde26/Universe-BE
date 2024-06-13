using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Events.Commands.CreateEvent;
using UniVerServer.Events.Commands.DeleteEvent;
using UniVerServer.Events.Commands.UpdateDate;
using UniVerServer.Events.Dto;
using UniVerServer.Events.Queries.GetEventById;
using UniVerServer.Events.Queries.GetEvents;
using UniVerServer.Subjects.DTO;

namespace UniVerServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class Events(IMediator mediator) : BaseController(mediator)
{ 
    private HttpResponseService responseService = new HttpResponseService();
    
    //CREATE 
    [HttpPost()]
    public async Task<ActionResult<ResponseDto>> CreateEvent([FromBody] CreateEventDto eventDetails) =>
        responseService.HandleResponse(await mediator.Send(new CreateEventCommand(eventDetails)));
    
    //READ 
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<GetSubjectDto>>> GetEvents() =>
        Ok(await mediator.Send(new GetEventsQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetSingleEventDto>> GetEvent(string id) =>
        Ok(await mediator.Send(new GetEventByIdQuery(Guid.Parse(id))));
    
    //UPDATE 
    // Re-assign organiser
    // Update date
    //body string format - YYYY-MM-DDTHH:MM:SSZ -> "2024-06-15T13:00:00Z"  ;
    [HttpPatch("Date/{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateEventDate(string id, [FromBody] string date) =>
        responseService.HandleResponse(
            await mediator.Send(new UpdateEventDateCommand(Guid.Parse(id), DateTime.Parse(date).ToUniversalTime())));
    // general update
    
 
    //    // PUT: api/Events/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutEvents(int id, Events events)
    //    {
    //        if (id != events.event_id)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(events).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!EventsExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //DELETE
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto>> PurgeEvent(string id) =>
        responseService.HandleResponse(await mediator.Send(new DeleteEventCommand(Guid.Parse(id))));
    
}

