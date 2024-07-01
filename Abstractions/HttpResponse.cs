using Microsoft.AspNetCore.Mvc;
using StatusCodes = UniVerServer.Enums.StatusCodes;

namespace UniVerServer.Abstractions;


public interface IHttpResponseService
{
    ActionResult HandleResponse(ResponseDto response);
}

public class HttpResponseService : ControllerBase, IHttpResponseService
{
    public ActionResult HandleResponse(ResponseDto response)
    {  
        
        // Switch to dictionary, more resource efficient (C# Dictionaries)
        // Dictionaries seem to be more memory hungry when not on a large scale?
        switch (response.StatusCode)
        {
            case StatusCodes.Ok:
                return Ok(response);
            case StatusCodes.NotFound:
                return NotFound(response);
            case StatusCodes.BadRequest:
                return BadRequest(response);
            case StatusCodes.Unprocessable:
                return UnprocessableEntity(response);
            case StatusCodes.Conflict:
                return Conflict(response);
            default:
                return StatusCode((int)response.StatusCode);
        }
    }
}