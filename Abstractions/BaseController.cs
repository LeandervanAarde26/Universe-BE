using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UniVerServer.Abstractions;

public abstract class BaseController : ControllerBase
{
    protected readonly IMediator Mediator;
    // protected IHttpResponseService Response;
    public BaseController(IMediator mediator
        // IHttpResponseService response
        )
    {
        Mediator = mediator;
        // Response = response;
    }
}