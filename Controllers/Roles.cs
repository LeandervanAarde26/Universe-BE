using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Roles.Commands.CreateRole;
using UniVerServer.Roles.Commands.PurgeRole;
using UniVerServer.Roles.Commands.UpdateRoleRate;
using UniVerServer.Roles.DTO;
using UniVerServer.Roles.Queries.GetAllRoles;
using UniVerServer.Roles.Queries.GetRoleById.IdentifierQuery;
using UniVerServer.Roles.Queries.GetRoleById.IdQuery;

namespace UniVerServer.Controllers;
    
[Route("api/[controller]")]
[ApiController]
public class Roles(IMediator mediator): BaseController(mediator)
{
    private HttpResponseService response = new HttpResponseService();
    
        // CREATE 
        [HttpPost()]
        public async Task<ActionResult<ResponseDto>> CreateRole([FromBody] CreateRoleDto role) =>
            response.HandleResponse(await mediator.Send(new CreateRoleCommand(role)));
        
        // READ
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UniVerServer.Roles.Models.Roles>>> ReadAllRoles() =>
            Ok(await mediator.Send(new GetRolesQuery()));
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UniVerServer.Roles.Models.Roles>> ReadSingleRole(string id) =>
            Ok(await mediator.Send(new GetRoleByIdQuery(Guid.Parse(id))));

        [HttpGet("Identifier/{identifier}")]
        public async Task<ActionResult<UniVerServer.Roles.Models.Roles>> ReadSingleRoleByIdentifier(string identifier) =>
            Ok(await mediator.Send(new GetRoleByIdentifierQuery(identifier)));
    
        // UPDATE
        [HttpPatch("{id}")]
        public async Task<ActionResult<ResponseDto>> UpdateRoleHourlyRate(string id, [FromBody] decimal rate) =>
            response.HandleResponse(await mediator.Send(new UpdateRoleRateCommand(Guid.Parse(id), rate)));
        
        // DELETE
        [HttpDelete("Purge/{id}")]
        public async Task<ActionResult<ResponseDto>> PurgeRole(string id) =>
            response.HandleResponse(await mediator.Send(new PurgeRoleCommand(Guid.Parse(id))));
        
}

