using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.Core;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost("/auth")]
        public async Task<ActionResult<AuthenticatedUser>> AuthenticateUser([FromBody] Authentication request)
        {
            if(string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password))
            {
                return BadRequest("Parameters to request can not be empty or Null");
            }
            // To lower not working now?
            var person = await _context.People.SingleOrDefaultAsync(p => p.person_email.ToLower().Equals(request.email.ToLower()));

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<People>>> GetStudents()
        {
            //var relationalData = await _context.People.Include(p => p.role).ToListAsync();
            var people = await (from person in _context.People
                                join role in _context.Roles
                                on person.role equals role.role_id
                                select new
                                {
                                    person.person_id,
                                    system_identifier = person.person_system_identifier,
                                    name = person.first_name + " " + person.last_name,
                                    email = person.person_email,
                                    role = role.role_name,
                                    credits = person.person_credits,
                                    neededCredits = person.needed_credits

                                })
                                .ToListAsync();
            if (_context.People == null)
          {
              return NotFound();
          }
          
          if (people == null)
          {
            return NotFound();
          }
          return Ok(people);
        }

        // GET: api/People/5
        [HttpGet("student/{id}")]
        public async Task<ActionResult<People>> GetStudent(string id)
        {
            if (_context.People == null)
             {
              return NotFound();
            }
            var person = await _context.People.Where(p => p.person_system_identifier.Equals(id)).FirstOrDefaultAsync();
            var enrolledSubjects = await _context.Courses.Where(c => c.student_id == id).ToListAsync();


            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        [HttpGet("lecturer/{id}")]
        public async Task<ActionResult<People>> GetLecturer(int id)
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

            return people;
        }

        //GET: Get person by role 
        [HttpGet("role/{role}")]
        public async Task<ActionResult<People>> GetPeopleWithRole(int role)
        {
            //There are a lot of Secuiry risks in my code in all honesty, please dont use this in industry, I will still try fix this from my end to make it as secure as possible.
            var people = await (from person in _context.People
                                join personRole in _context.Roles
                                on person.role equals personRole.role_id
                                where person.role == role
                                select new
                                {
                                    person.person_id,
                                    system_identifier = person.person_system_identifier,
                                    name = person.first_name + " " + person.last_name,
                                    email = person.person_email,
                                    role = personRole.role_name,
                                    credits = person.person_credits,
                                    neededCredits = person.needed_credits

                                }).ToListAsync();


            if (_context.People == null)
            {
                return NotFound();
            }
        

            if (people == null)
            {
                return NotFound();
            }

            return Ok(people);
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

            if (people.role > 3)
            {
                people.role = 3;
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
