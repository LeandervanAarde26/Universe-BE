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
    public class MadePaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MadePaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MadePayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MadePayments>>> GetMadePayments()
        {
          if (_context.MadePayments == null)
          {
              return NotFound();
          }
            return await _context.MadePayments.ToListAsync();
        }

        // GET: api/MadePayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MadePayments>> GetMadePayments(int id)
        {
          if (_context.MadePayments == null)
          {
              return NotFound();
          }
            var madePayments = await _context.MadePayments.FindAsync(id);

            if (madePayments == null)
            {
                return NotFound();
            }

            return madePayments;
        }

        // PUT: api/MadePayments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMadePayments(int id, MadePayments madePayments)
        {
            if (id != madePayments.payment_id)
            {
                return BadRequest();
            }

            _context.Entry(madePayments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MadePaymentsExists(id))
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

        // POST: api/MadePayments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MadePayments>> PostMadePayments(MadePayments madePayments)
        {
          if (_context.MadePayments == null)
          {
              return Problem("Entity set 'ApplicationDbContext.MadePayments'  is null.");
          }
            _context.MadePayments.Add(madePayments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMadePayments", new { id = madePayments.payment_id }, madePayments);
        }

        // DELETE: api/MadePayments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMadePayments(int id)
        {
            if (_context.MadePayments == null)
            {
                return NotFound();
            }
            var madePayments = await _context.MadePayments.FindAsync(id);
            if (madePayments == null)
            {
                return NotFound();
            }

            _context.MadePayments.Remove(madePayments);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MadePaymentsExists(int id)
        {
            return (_context.MadePayments?.Any(e => e.payment_id == id)).GetValueOrDefault();
        }
    }
}
