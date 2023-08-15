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
    public class OutStandingStudentFeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OutStandingStudentFeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OutStandingStudentFees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutStandingStudentFees>>> GetOutStandingStudentFees()
        {
          if (_context.OutStandingStudentFees == null)
          {
              return NotFound();
          }
            return await _context.OutStandingStudentFees.ToListAsync();
        }

        // GET: api/OutStandingStudentFees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OutStandingStudentFees>> GetOutStandingStudentFees(int id)
        {
          if (_context.OutStandingStudentFees == null)
          {
              return NotFound();
          }
            var outStandingStudentFees = await _context.OutStandingStudentFees.FindAsync(id);

            if (outStandingStudentFees == null)
            {
                return NotFound();
            }

            return outStandingStudentFees;
        }

        // PUT: api/OutStandingStudentFees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOutStandingStudentFees(int id, OutStandingStudentFees outStandingStudentFees)
        {
            if (id != outStandingStudentFees.fee_id)
            {
                return BadRequest();
            }

            _context.Entry(outStandingStudentFees).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OutStandingStudentFeesExists(id))
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

        // POST: api/OutStandingStudentFees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OutStandingStudentFees>> PostOutStandingStudentFees(OutStandingStudentFees outStandingStudentFees)
        {
          if (_context.OutStandingStudentFees == null)
          {
              return Problem("Entity set 'ApplicationDbContext.OutStandingStudentFees'  is null.");
          }
            _context.OutStandingStudentFees.Add(outStandingStudentFees);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOutStandingStudentFees", new { id = outStandingStudentFees.fee_id }, outStandingStudentFees);
        }

        // DELETE: api/OutStandingStudentFees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOutStandingStudentFees(int id)
        {
            if (_context.OutStandingStudentFees == null)
            {
                return NotFound();
            }
            var outStandingStudentFees = await _context.OutStandingStudentFees.FindAsync(id);
            if (outStandingStudentFees == null)
            {
                return NotFound();
            }

            _context.OutStandingStudentFees.Remove(outStandingStudentFees);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OutStandingStudentFeesExists(int id)
        {
            return (_context.OutStandingStudentFees?.Any(e => e.fee_id == id)).GetValueOrDefault();
        }
    }
}
