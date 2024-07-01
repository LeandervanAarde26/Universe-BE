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
    //TODO: Rename controller file (Done) 
    //TODO: REDO all endpoints
    // TODO: Cleanup models for users and update database. 
    // TODO: Seed database 
    // TODO: Add Auth middleware. 
    
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
        
        // [HttpPost("auth")]
        // public async Task<ActionResult<AuthenticatedUser>> AuthenticateUser([FromBody] Authentication request)
        // {
        //     if(string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password))
        //     {
        //         return BadRequest("Parameters to request can not be empty or Null");
        //     }
        //     // To lower not working now?
        //     var person = await _context.People.SingleOrDefaultAsync(p => p.person_email.Equals(request.email));
        //
        //     if (person == null || person.person_active == false)
        //     {
        //         return NotFound();
        //     }
        //
        //     AuthenticatedUser formattedPerson = new()
        //     {
        //         user_id = person.person_id,
        //         username = $"{person.first_name} {person.last_name}",
        //         userEmail = person.person_email
        //     };
        //
        //     if (formattedPerson == null)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError);
        //     }
        //
        //     var roles = await _context.Roles.SingleOrDefaultAsync(p => p.role_id.Equals(person.role));
        //
        //     var isAuthenticated = ValidateUserCredentials(person.person_password, request.password, request.email);
        //
        //     if (!isAuthenticated || !roles!.can_access || !person.person_active)
        //     {
        //         return Unauthorized();
        //     }
        //     return Ok(formattedPerson);
        // }
        //

       
        // [HttpGet()]
        // public async Task<ActionResult<People>> GetLoggedInUser(int userId)
        // {
        //     if (_context.People == null)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError);
        //     }
        //
        //     if(int.IsNegative(userId) || userId == null)
        //     {
        //         return BadRequest("Invalid request parameters");
        //     }
        //
        //     var user = await _context.People.SingleOrDefaultAsync(p => p.person_id.Equals(userId));
        //
        //     if (user == null)
        //     {
        //         return NotFound("User not found on database");
        //     }
        //
        //     return user;
        // }
        //
        // [HttpGet("AdminFees")] 
        // public async Task<ActionResult<People>> GetAdminSalary()
        // {
        //     if(_context.People == null)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError);
        //     }
        //
        //     var adminFees = await (from admin in _context.People
        //                            join role in _context.Roles
        //                            on admin.role equals role.role_id
        //                            where admin.role == 1
        //                            select new
        //                            {
        //                                admin_id = admin.person_id,
        //                                admin_name = admin.first_name + " " + admin.last_name,
        //                                admin_payment = role.rate,
        //
        //                            }).ToListAsync();
        //
        //
        //     if(adminFees ==  null)
        //     {
        //         return NotFound();
        //     }
        //
        //
        //     return Ok(adminFees);
        // }
        //
     
        //
        // // GET: api/People/5
        // [HttpGet("student/{id}")]
        // public async Task<ActionResult<SingleStudentWithCourses>> GetStudent(int id)
        // {
        //     var personWithSubject = await (from student in _context.People
        //                                    join role in _context.Roles
        //                                    on student.role equals role.role_id
        //                                    where student.person_id== id
        //                                    select new
        //                                    {
        //                                        student_id = student.person_id,
        //                                        student_Stnumber = student.person_system_identifier,
        //                                        student_name = student.first_name + " " + student.last_name,
        //                                        student_email = student.person_email,
        //                                        student_Cell = student.person_cell,
        //                                        studentRole_nr = student.role,
        //                                        student_role = role.role_name,
        //                                        student_credits = student.person_credits,
        //                                        outstanding_credits = student.needed_credits
        //                                    })
        //                                    .FirstOrDefaultAsync();
        //
        //     if (personWithSubject == null)
        //     {
        //         return NotFound();
        //     }
        //
        //
        //     if (personWithSubject.studentRole_nr < 3)
        //     {
        //         return BadRequest("Please enter Student id");
        //     }
        //
        //     var enrollments = await (from course in _context.Courses
        //                              join subject in _context.Subjects
        //                              on course.Subjects equals subject.subject_id
        //                              where course.student_id == personWithSubject.student_Stnumber
        //                              select new SubjectEnrollments
        //                              {
        //                                  subject_id = subject.subject_id,
        //                                  subject_name = subject.subject_name,
        //                                  subject_code = subject.subject_code,
        //                                  subject_color = subject.subject_color,
        //                              })
        //                            .ToListAsync();
        //
        //     var individualStudent = new SingleStudentWithCourses
        //     {
        //         student_id = personWithSubject.student_id,
        //         person_system_identifier = personWithSubject.student_Stnumber,
        //         student_name = personWithSubject.student_name,
        //         email = personWithSubject.student_email,
        //         student_phoneNumber = personWithSubject.student_Cell,
        //         role = personWithSubject.student_role,
        //         person_credits = personWithSubject.student_credits,
        //         needed_credits = personWithSubject.outstanding_credits,
        //         enrollments = enrollments
        //     };
        //
        //     return Ok(individualStudent);
        // }
        //
        //
        // [HttpGet("Lecturer/{id}")]
        // public async Task<ActionResult<People>> GetLecturer(int id)
        // {
        //     if (_context.People == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     var lecturer = await (from lect in _context.People
        //                           join lecturerRole in _context.Roles
        //                           on lect.role equals lecturerRole.role_id
        //                           join subject in _context.Subjects 
        //                           on lect.person_id equals subject.lecturer_id into subjectGroup
        //                           from subject in subjectGroup.DefaultIfEmpty()
        //                           where lect.person_id == id
        //                           select new
        //                           {
        //                               lect_id = lect.person_id,
        //                               image = "",
        //                               name = lect.first_name + " " + lect.last_name,
        //                               email = lect.person_email,
        //                               phone = lect.person_cell,
        //                               role = lecturerRole.role_name,
        //                               rate = lecturerRole.rate,
        //                               subject_id = subject.subject_id != null ? subject.subject_id : 0,
        //                               subject_name = subject.subject_name != null ? subject.subject_name : null,  
        //                               subject_code = subject.subject_code != null ? subject.subject_code : null,  
        //                               subject_color = subject.subject_color != null ? subject.subject_color : null,
        //                           }).ToListAsync();
        //
        //     var singleLecturerWithCourses = lecturer
        //                             .GroupBy(lecturer => lecturer.name)
        //                             .Select(group => new LecturerWithCourses
        //                             {
        //                                 lecturer_id = group.First().lect_id,
        //                                 lecturer_name = group.First().name,
        //                                 email = group.First().email,
        //                                 lecturer_phoneNumber = group.First().phone,
        //                                 role = group.First().role,
        //                                 lecturer_rate = group.First().rate,
        //                                 enrollments = group.Select(e => new SubjectEnrollments
        //                                 {
        //                                     subject_id = e.subject_id,
        //                                     subject_name = e.subject_name,
        //                                     subject_code = e.subject_code,
        //                                     subject_color = e.subject_color,
        //                                 }).ToList()
        //
        //                             })
        //                             .FirstOrDefault(); ;
        //     if (lecturer == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     if (singleLecturerWithCourses == null || (singleLecturerWithCourses.role != "Lecturer" && singleLecturerWithCourses.role != "Admin"))
        //     {
        //         return BadRequest("Please enter valid details");
        //     }
        //     return Ok(singleLecturerWithCourses);
        // }
        //
        
    }
}
