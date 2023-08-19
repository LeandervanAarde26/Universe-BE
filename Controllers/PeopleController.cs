using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.Core;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using UniVerServer;
using UniVerServer.Models;
using UniVerServer.Models.CustomDataObjects;

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
        public async Task<ActionResult<PersonDataObject>> AuthenticateUser([FromBody] Authentication request)
        {
            var person = await _context.People.Where(p => p.person_email.Equals(request.email)).FirstOrDefaultAsync();
            var roles = await _context.Roles.Where(p => p.role_id.Equals(person.role)).FirstOrDefaultAsync();

            if (person == null || person.person_active == false )
            {
                return NotFound();
            }
           
            var isAuthenticated = ValidateUserCredentials(person.person_password, request.password, request.email);

            if (!isAuthenticated || !roles!.can_access || !person.person_active)
            {
                return Unauthorized();
            }
            return Ok(true);
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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<People>>> GetPeople()
        {
          if (_context.People == null)
          {
              return NotFound();
          }
          
          var relationalData = await _context.People.Include(p => p.role).ToListAsync();

          if (relationalData == null)
          {
            return NotFound();
          }
          return relationalData;
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
            IEnumerable<People> people  = await _context.People.Include(p => p.role).Where(p => p.role == role).ToListAsync();
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
            var person = await _context.People.Where(p => p.person_email.Equals(people.person_email)).FirstOrDefaultAsync();

            if(person != null)
            {
                return Conflict();
            }

            if (_context.People == null)
          {
              return Problem("Entity set 'ApplicationDbContext.People'  is null.");
          }
            people.person_password = Argon2.Hash(people.person_password); //Hashing the password before adding the person (Linear programming)

            if(people.role > 3)
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
