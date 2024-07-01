using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Events.Commands.CreateEvent;
using UniVerServer.Events.Commands.DeleteEvent;
using UniVerServer.Events.Commands.UpdateDate;
using UniVerServer.Events.Commands.UpdateEvent;
using UniVerServer.Events.Commands.UpdateHost;
using UniVerServer.Events.Dto;
using UniVerServer.Events.Queries.GetEventById;
using UniVerServer.Events.Queries.GetEvents;
using UniVerServer.Events.Queries.GetEventsInMonth;
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
    public async Task<ActionResult<IEnumerable<GetEventsDto>>> GetEvents() =>
        Ok(await mediator.Send(new GetEventsQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<GetSingleEventDto>> GetEvent(string id) =>
        Ok(await mediator.Send(new GetEventByIdQuery(Guid.Parse(id))));
    
    //string format - YYYY-MM-DD -> "2024-06-15"  ;
    [HttpGet("Month/{date}")]
    public async Task<ActionResult<IEnumerable<GetEventsDto>>> GetEventsInMonth(string date) =>
        Ok(await mediator.Send(new GetEventsInMonthQuery(DateTime.Parse(date).ToUniversalTime())));
    //UPDATE 
    // Re-assign organiser
    [HttpPatch("Organiser/{eventId}")]
    public async Task<ActionResult<ResponseDto>> UpdateEventOrganiser( string eventId,
        [FromBody] string organiserId)
    {
        UpdateEventHostDto data = new UpdateEventHostDto
            { EventId = Guid.Parse(eventId), NewHost = Guid.Parse(organiserId) };
        return responseService.HandleResponse(await mediator.Send(new UpdateHostCommand(data)));
    }
    
    // Update date
    //body string format - YYYY-MM-DDTHH:MM:SSZ -> "2024-06-15T13:00:00Z"  ;
    [HttpPatch("Date/{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateEventDate(string id, [FromBody] string date) =>
        responseService.HandleResponse(
            await mediator.Send(new UpdateEventDateCommand(Guid.Parse(id), DateTime.Parse(date).ToUniversalTime())));
    
    // general update
    // See UpdateEventDto for updatable fields.
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto>> UpdateEvent(string id, [FromBody] UpdateEventDto eventDetails) =>
        responseService.HandleResponse(await mediator.Send(new UpdateEventCommand(eventDetails, Guid.Parse(id))));
    

    //DELETE
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDto>> PurgeEvent(string id) =>
        responseService.HandleResponse(await mediator.Send(new DeleteEventCommand(Guid.Parse(id))));
    
}

