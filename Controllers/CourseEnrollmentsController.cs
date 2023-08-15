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
    public class CourseEnrollmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CourseEnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CourseEnrollments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseEnrollments>>> GetCourses()
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            return await _context.Courses.ToListAsync();
        }

        // GET: api/CourseEnrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseEnrollments>> GetCourseEnrollments(int id)
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            var courseEnrollments = await _context.Courses.FindAsync(id);

            if (courseEnrollments == null)
            {
                return NotFound();
            }

            return courseEnrollments;
        }

        // PUT: api/CourseEnrollments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourseEnrollments(int id, CourseEnrollments courseEnrollments)
        {
            if (id != courseEnrollments.enrollment_id)
            {
                return BadRequest();
            }

            _context.Entry(courseEnrollments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseEnrollmentsExists(id))
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

        // POST: api/CourseEnrollments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CourseEnrollments>> PostCourseEnrollments(CourseEnrollments courseEnrollments)
        {
          if (_context.Courses == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Courses'  is null.");
          }
            _context.Courses.Add(courseEnrollments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseEnrollments", new { id = courseEnrollments.enrollment_id }, courseEnrollments);
        }

        // DELETE: api/CourseEnrollments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseEnrollments(int id)
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var courseEnrollments = await _context.Courses.FindAsync(id);
            if (courseEnrollments == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(courseEnrollments);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseEnrollmentsExists(int id)
        {
            return (_context.Courses?.Any(e => e.enrollment_id == id)).GetValueOrDefault();
        }
    }
}
