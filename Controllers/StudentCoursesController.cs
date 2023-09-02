using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniVerServer;
using UniVerServer.Models;

namespace UniVerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourses>>> GetStudentCourses()
        {
          if (_context.StudentCourses == null)
          {
              return NotFound();
          }
            return await _context.StudentCourses.ToListAsync();
        }
        // GET: api/StudentCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentCourses>> GetStudentCourses(string id)
        {
          if (_context.StudentCourses == null)
          {
              return NotFound();
          }
            var studentCourses = await _context.StudentCourses.FindAsync(id);

            if (studentCourses == null)
            {
                return NotFound();
            }

            return studentCourses;
        }

        // PUT: api/StudentCourses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentCourses(string id, StudentCourses studentCourses)
        {
            if (id != studentCourses.grade_id)
            {
                return BadRequest();
            }

            _context.Entry(studentCourses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentCoursesExists(id))
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

        // POST: api/StudentCourses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentCourses>> PostStudentCourses(StudentCourses studentCourses)
        {
          if (_context.StudentCourses == null)
          {
              return Problem("Entity set 'ApplicationDbContext.StudentCourses'  is null.");
          }
            _context.StudentCourses.Add(studentCourses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentCourses", new { id = studentCourses.grade_id }, studentCourses);
        }

        // DELETE: api/StudentCourses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentCourses(string id)
        {
            if (_context.StudentCourses == null)
            {
                return NotFound();
            }
            var studentCourses = await _context.StudentCourses.FindAsync(id);
            if (studentCourses == null)
            {
                return NotFound();
            }

            _context.StudentCourses.Remove(studentCourses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentCoursesExists(string id)
        {
            return (_context.StudentCourses?.Any(e => e.grade_id == id)).GetValueOrDefault();
        }


    }
}
