using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Users.Commands.CreateUser;
using UniVerServer.Users.Commands.DeleteUser;
using UniVerServer.Users.Commands.PurgeUser;
using UniVerServer.Users.Commands.SetUserActive;
using UniVerServer.Users.Commands.UpdatePassword;
using UniVerServer.Users.Commands.UpdatePhoneNumber;
using UniVerServer.Users.Commands.UpdateUser;
using UniVerServer.Users.DTO;
using UniVerServer.Users.Queries.GetAllStaffMembers;
using UniVerServer.Users.Queries.GetAllStudents;
using UniVerServer.Users.Queries.GetById;

namespace UniVerServer.Controllers
{
    // TODO:
    // Rename controller file (Done) 
    // REDO all endpoints
    // Cleanup models for users and update database. (Done) 
    // Seed database (Done)
    // Add Auth middleware.
    // AUTH CONTROLLER 
    // EXTRACT AUTH TO EXTERNAL SERVICE
    // GET STUDENTS WITH THEIR ENROLLMENTS -> Enrollments controller or Users controller? 
    // GET LECTURER WITH THEIR SUBJECTS -> Subjects controller or Users controller? 
    
    [Route("api/[controller]")]
    [ApiController]
    public class People(IMediator mediator) : BaseController(mediator)
    {
        private HttpResponseService response = new HttpResponseService();
        // CREATE
        [HttpPost("")]
        public async Task<ActionResult<ResponseDto>> CreateUser([FromBody] CreateUserDto user) =>
            response.HandleResponse(await mediator.Send(new CreateUserCommand(user)));
        // READ
        // Get all staff (optional, use parameter * for all)
        //Get all staff by their role
        [HttpGet("staff/{role}")]
        public async Task<ActionResult<IEnumerable<GetStaffMembersDto>>> ReadStaffMembers(string role = "*") =>
            Ok(await mediator.Send(new GetStaffMemberQuery(role)));
        
        // Get all students (optional, use parameter * for all)
        // Get all students by their role
        [HttpGet("Students/{role}")]
        public async Task<ActionResult<IEnumerable<GetStaffMembersDto>>> ReadStudents(string role = "*") =>
            Ok(await mediator.Send(new GetStudentsQuery(role)));


        [HttpGet("{Id}")]
        public async Task<ActionResult<GetUserByIdDto>> ReadUserById(string Id) =>
            Ok(await mediator.Send(new GetUserByIdQuery(Guid.Parse(Id))));
        
        // UPDATE
        [HttpPatch("PhoneNumber/{id}")]
        public async Task<ActionResult<ResponseDto>> UpdatePhoneNumber(string id, [FromBody] string phoneNumber) =>
            response.HandleResponse(await mediator.Send(new UpdateUserPhoneNumberCommand(new UpdatePhoneNumberDto(Guid.Parse(id), phoneNumber))));

        [HttpPatch("SetActive/{id}")]
        public async Task<ActionResult<ResponseDto>> UpdateUserToActive(string id) =>
            response.HandleResponse(await mediator.Send(new SetUserActiveCommand(Guid.Parse(id))));
        
        // Needs middleware to ensure only logged in user and superadmin can do it.
        [HttpPatch("Password/{id}")]
        public async Task<ActionResult<ResponseDto>> UpdateUserPassword(string id, [FromBody] string password) =>
            response.HandleResponse(await mediator.Send(new UpdateUserPasswordCommand(Guid.Parse(id), password)));

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> UpdateUser(string id, [FromBody] UpdateUserDto user) =>
            response.HandleResponse(await mediator.Send(new UpdateUserCommand(Guid.Parse(id), user)));
        
        //DELETE
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseDto>> DeleteUser(string Id) =>
            response.HandleResponse(await mediator.Send(new DeleteUserCommand(Guid.Parse(Id))));
        
        //purge user
        [HttpDelete("purge/{Id}")]
        public async Task<ActionResult<ResponseDto>> PurgeUser(string Id) =>
            response.HandleResponse(await mediator.Send(new PurgeUserCommand(Guid.Parse(Id))));
    }
}
