using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniVerServer.Abstractions;
using UniVerServer.Users.Commands.CreateUser;
using UniVerServer.Users.Commands.DeleteUser;
using UniVerServer.Users.Commands.PurgeUser;
using UniVerServer.Users.DTO;
using UniVerServer.Users.Queries.GetAllStaffMembers;
using UniVerServer.Users.Queries.GetAllStudents;

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
        
        // UPDATE
        
        
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
        // private bool ValidateUserCredentials(string password,  string person_password, string person_email)
        // {
        //     var user = _context.People.FirstOrDefault(p => p.person_email == person_email);
        //
        //     if (user != null)
        //     {
        //         if (Argon2.Verify(password, person_password))
        //         {
        //             return true;
        //         }
        //     }
        //
        //     return false;
        // }
        //
        // [HttpPut("Password")]
        // public async Task<IActionResult> UpdateUserPassword([FromBody] Authentication request)
        // {
        //     if (string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password))
        //     {
        //         return BadRequest("Invalid request parameters.");
        //     }
        //
        //     var existingUser = await _context.People
        //         .SingleOrDefaultAsync(p => p.person_email.ToLower().Equals(request.email.ToLower()));
        //
        //     if (existingUser == null)
        //     {
        //         return NotFound("Employee not found.");
        //     }
        //
        //     existingUser.person_password = Argon2.Hash(request.password);
        //
        //     int rowsAffected = await _context.SaveChangesAsync();
        //
        //     if (rowsAffected < 1)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the password.");
        //     }
        //
        //     return Ok(true);
        // }
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
      
       
        // // PUT: api/People/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutPeople(int id, People people)
        // {
        //     if (id != people.person_id)
        //     {
        //         return BadRequest();
        //     }
        //
        //     _context.Entry(people).State = EntityState.Modified;
        //
        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!PeopleExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }
        //
        //     return NoContent();
        // }
        //
        //
        // [HttpPut("SetActive")]
        // public async Task<IActionResult> SetActiveState([FromBody] int Id)
        // {
        //  
        //     if(_context.People == null)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError);
        //     }
        //
        //     var existingUser = await _context.People
        //         .SingleOrDefaultAsync(p => p.person_id.Equals(Id));
        //
        //     if (existingUser == null)
        //     {
        //         return NotFound("Person not found.");
        //     }
        //
        //     existingUser.person_active = !existingUser.person_active;
        //
        //     int rowsAffected = await _context.SaveChangesAsync();
        //
        //     if (rowsAffected < 1)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the user.");
        //     }
        //
        //     return Ok(true);
        // }
        //
        //
        // [HttpPut("UpdateNumber")]
        // public async Task<IActionResult> ChangePhoneNumber([FromBody] PhoneNumberUpdateModel model)
        // {
        //     if (_context.People == null)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError);
        //     }
        //
        //     var existingUser = await _context.People
        //         .SingleOrDefaultAsync(p => p.person_id.Equals(model.Id));
        //
        //     if (existingUser == null)
        //     {
        //         return NotFound("Person not found.");
        //     }
        //
        //     existingUser.person_cell = model.PhoneNumber;
        //
        //     int rowsAffected = await _context.SaveChangesAsync();
        //
        //     if (rowsAffected < 1)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the user.");
        //     }
        //
        //     return Ok(true);
        // }
    }
}
