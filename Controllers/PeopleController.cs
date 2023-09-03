using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.Core;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using UniVerServer;
using UniVerServer.Models;
using UniVerServer.Models.CustomDataObjects;
using UniVerServer.Models.DTO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UniVerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("auth")]
        public async Task<ActionResult<AuthenticatedUser>> AuthenticateUser([FromBody] Authentication request)
        {
            if(string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password))
            {
                return BadRequest("Parameters to request can not be empty or Null");
            }
            // To lower not working now?
            var person = await _context.People.SingleOrDefaultAsync(p => p.person_email.Equals(request.email));

            if (person == null || person.person_active == false)
            {
                return NotFound();
            }

            AuthenticatedUser formattedPerson = new()
            {
                user_id = person.person_id,
                username = $"{person.first_name} {person.last_name}",
                userEmail = person.person_email
            };

            if (formattedPerson == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var roles = await _context.Roles.SingleOrDefaultAsync(p => p.role_id.Equals(person.role));

            var isAuthenticated = ValidateUserCredentials(person.person_password, request.password, request.email);

            if (!isAuthenticated || !roles!.can_access || !person.person_active)
            {
                return Unauthorized();
            }
            return Ok(formattedPerson);
        }

        private bool ValidateUserCredentials(string password,  string person_password, string person_email)
        {
            var user = _context.People.FirstOrDefault(p => p.person_email == person_email);

            if (user != null)
            {
                if (Argon2.Verify(password, person_password))
                {
                    return true;
                }
            }

            return false;
        }

        [HttpPut("Password")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] Authentication request)
        {
            if (string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password))
            {
                return BadRequest("Invalid request parameters.");
            }

            var existingUser = await _context.People
                .SingleOrDefaultAsync(p => p.person_email.ToLower().Equals(request.email.ToLower()));

            if (existingUser == null)
            {
                return NotFound("Employee not found.");
            }

            existingUser.person_password = Argon2.Hash(request.password);

            int rowsAffected = await _context.SaveChangesAsync();

            if (rowsAffected < 1)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the password.");
            }

            return Ok(true);
        }
        [HttpGet()]
        public async Task<ActionResult<People>> GetLoggedInUser(int userId)
        {
            if (_context.People == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if(int.IsNegative(userId) || userId == null)
            {
                return BadRequest("Invalid request parameters");
            }

            var user = await _context.People.SingleOrDefaultAsync(p => p.person_id.Equals(userId));

            if (user == null)
            {
                return NotFound("User not found on database");
            }

            return user;
        }

        [HttpGet("AdminFees")] 
        public async Task<ActionResult<People>> GetAdminSalary()
        {
            if(_context.People == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var adminFees = await (from admin in _context.People
                                   join role in _context.Roles
                                   on admin.role equals role.role_id
                                   where admin.role == 1
                                   select new
                                   {
                                       admin_id = admin.person_id,
                                       admin_name = admin.first_name + " " + admin.last_name,
                                       admin_payment = role.rate,

                                   }).ToListAsync();


            if(adminFees ==  null)
            {
                return NotFound();
            }


            return Ok(adminFees);
        }

        [HttpGet("Students")]
        public async Task<ActionResult<People>> GetAllStudents()
        {
            if (_context.People == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var students = await (from student in _context.People
                                   join studentRole in _context.Roles
                                   on student.role equals studentRole.role_id
                                   where student.role > 2
                                   select new
                                   {
                                       id = student.person_id,
                                       image = "",
                                       name = student.first_name + " " + student.last_name,
                                       email = student.person_email,
                                       role = studentRole.role_name,
                                       
                                       person_credits = student.person_credits,
                                       needed_credits = student.needed_credits,
                                       person_system_identifier = student.person_system_identifier,
                                       //subject = "N/A"
                                   }).ToListAsync();
            if (students == null)
            {
                return NotFound("No Students on the system");
            }

            return Ok(students);
        }

        // GET: api/People/5
        [HttpGet("student/{id}")]
        public async Task<ActionResult<People>> GetStudent(string id)
        {
            if (_context.People == null)
             {
              return NotFound();
            }
            //var person = await _context.People.Where(p => p.person_system_identifier.Equals(id)).FirstOrDefaultAsync();
            //var enrolledSubjects = await _context.Courses.Where(c => c.student_id == id).ToListAsync();

            var personWithSubject = await (from student in _context.People
                                           join course in _context.Courses
                                           on student.person_system_identifier equals course.student_id
                                           join subject in _context.Subjects
                                           on course.Subjects equals subject.subject_id
                                           join role in _context.Roles
                                           on student.role equals role.role_id  
                                           where student.person_system_identifier == id
                                           select new
                                           {
                                               student_id = student.person_id,
                                               student_Stnumber = student.person_system_identifier,
                                               student_name = student.first_name +" "+ student.last_name,
                                               student_email = student.person_email,
                                               student_Cell = student.person_cell,
                                               student_role = role.role_name,
                                               student_credits = student.person_credits,
                                               outstanding_credits = student.needed_credits,
                                               subject_id = subject.subject_id,
                                               subject_name = subject.subject_name,
                                               subject_code = subject.subject_code,
                                               subject_color = subject.subject_color,
                                           })
                                           .ToListAsync();

            var individualStudent = personWithSubject
                                    .GroupBy(student => student.student_name)
                                    .Select(group => new SingleStudentWithCourses
                                    {
                                        student_id = group.First().student_id,
                                        person_system_identifier = group.First().student_Stnumber,
                                        student_name = group.First().student_name,
                                        email = group.First().student_email,
                                        student_phoneNumber = group.First().student_Cell,
                                        role = group.First().student_role,
                                        person_credits = group.First().student_credits,
                                        needed_credits = group.First().outstanding_credits,
                                        enrollments = group.Select(e => new SubjectEnrollments
                                        {
                                            subject_id = e.subject_id,
                                            subject_name = e.subject_name,
                                            subject_code=e.subject_code,
                                            subject_color = e.subject_color,    
                                        }).ToList()

                                    })
                                    .FirstOrDefault();


            //if (person == null)
            //{
            //    return NotFound();
            //}
            return Ok(individualStudent);
        }

        [HttpGet("Lecturer/{id}")]
        public async Task<ActionResult<People>> GetLecturer(int id)
        {
            if (_context.People == null)
            {
                return NotFound();
            }

            var lecturer = await (from lect in _context.People
                                  join lecturerRole in _context.Roles
                                  on lect.role equals lecturerRole.role_id
                                  where lect.person_id == id
                                  select new
                                  {
                                      id = lect.person_id,
                                      image = "",
                                      name = lect.first_name + " " + lect.last_name,
                                      email = lect.person_email,
                                      role = lecturerRole.role_name,
                                      subject = "N/A"
                                  }).SingleAsync();
            if (lecturer == null)
            {
                return NotFound();
            }

            return Ok(lecturer);
        }

        [HttpGet("Lecturers")]
        public async Task<ActionResult<People>> GetAllLecturers()
        {
            if (_context.People == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var lecturers = await (from lecturer in _context.People
                               join lecturerRole in _context.Roles
                               on lecturer.role equals lecturerRole.role_id
                               where lecturer.role == 2
                               select new
                               {
                                   id = lecturer.person_id,
                                   image = "",
                                   name = lecturer.first_name + " " + lecturer.last_name,
                                   role = lecturerRole.role_name,
                                   subject = "N/A"
                               }).ToListAsync();

            if (lecturers == null)
            {
                return NotFound("No lecturers on the system");
            }
         

            return Ok(lecturers);
        }

        [HttpGet("Staff")]
        public async Task<ActionResult<People>> GetStaffMembers()
        {
            if (_context.People == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError) ;
            }


            var st = await (from staff in _context.People
                            join staffRole in _context.Roles
                            on staff.role equals staffRole.role_id
                            where staff.role < 3
                            select new
                            {
                                id = staff.person_id,
                                image = "",
                                name = staff.first_name + " " + staff.last_name,
                                role = staffRole.role_name,
                                subject = "N/A"
                            }).ToListAsync();

           
            if(st == null)
            {
                return NotFound();
            }

            return Ok(st);
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeople(int id, People people)
        {
            if (id != people.person_id)
            {
                return BadRequest();
            }

            _context.Entry(people).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<People>> PostPeople(People people)
        {
            if (people == null)
            {
                return BadRequest("Provided People object is null.");
            }

            bool emailExists = await _context.People.AnyAsync(p => p.person_email.Equals(people.person_email));
            if (emailExists)
            {
                return Conflict("A person with the same email already exists.");
            }

            people.person_password = Argon2.Hash(people.person_password);

            if (people.role > 4)
            {
                people.role = 4;
            }


            _context.People.Add(people);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPeople", new { id = people.person_id }, people);
        }

            // DELETE: api/People/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeople(int id)
        {
            if (_context.People == null)
            {
                return NotFound();
            }
            var people = await _context.People.FindAsync(id);
        
            if (people == null)
            {
                return NotFound();
            }

            _context.People.Remove(people);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeopleExists(int id)
        {
            return (_context.People?.Any(e => e.person_id == id)).GetValueOrDefault();
        }
    }
}
