// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using UniVerServer;
// using UniVerServer.Models;
// using UniVerServer.Models.DTO;
//
// namespace UniVerServer.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class EventsController : ControllerBase
//     {
//         private readonly ApplicationDbContext _context;
//
//         public EventsController(ApplicationDbContext context)
//         {
//             _context = context;
//         }
//
//         // GET: api/Events
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Events>>> GetEvents()
//         {
//
//
//           if (_context.Events == null)
//           {
//                 return StatusCode(StatusCodes.Status500InternalServerError);
//           }
//
//           var events = await (from evs in _context.Events 
//                               join organiser in _context.People
//                               on evs.staff_organiser equals organiser.person_id
//                               select new EventsDTO()
//                               {
//                                   event_id = evs.event_id,
//                                   event_name = evs.event_name,
//                                   event_description = evs.event_description,
//                                   staff_organiser = organiser.first_name + " " + organiser.last_name,
//                                   event_date = evs.event_date.ToString("dddd, dd MMMM yyyy")
//                               }).ToListAsync();
//
//             return Ok(events);
//         }
//
//         // GET: api/Events/5
//     //    [HttpGet("{id}")]
//     //    public async Task<ActionResult<Events>> GetEvents(int id)
//     //    {
//     //      if (_context.Events == null)
//     //      {
//     //          return NotFound();
//     //      }
//     //        var events = await _context.Events.FindAsync(id);
//
//     //        if (events == null)
//     //        {
//     //            return NotFound();
//     //        }
//
//     //        return events;
//     //    }
//
//     //    // PUT: api/Events/5
//     //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//     //    [HttpPut("{id}")]
//     //    public async Task<IActionResult> PutEvents(int id, Events events)
//     //    {
//     //        if (id != events.event_id)
//     //        {
//     //            return BadRequest();
//     //        }
//
//     //        _context.Entry(events).State = EntityState.Modified;
//
//     //        try
//     //        {
//     //            await _context.SaveChangesAsync();
//     //        }
//     //        catch (DbUpdateConcurrencyException)
//     //        {
//     //            if (!EventsExists(id))
//     //            {
//     //                return NotFound();
//     //            }
//     //            else
//     //            {
//     //                throw;
//     //            }
//     //        }
//
//     //        return NoContent();
//     //    }
//
//     //    // POST: api/Events
//     //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//     //    [HttpPost]
//     //    public async Task<ActionResult<Events>> PostEvents(Events events)
//     //    {
//     //      if (_context.Events == null)
//     //      {
//     //          return Problem("Entity set 'ApplicationDbContext.Events'  is null.");
//     //      }
//     //        _context.Events.Add(events);
//     //        await _context.SaveChangesAsync();
//
//     //        return CreatedAtAction("GetEvents", new { id = events.event_id }, events);
//     //    }
//
//     //    // DELETE: api/Events/5
//     //    [HttpDelete("{id}")]
//     //    public async Task<IActionResult> DeleteEvents(int id)
//     //    {
//     //        if (_context.Events == null)
//     //        {
//     //            return NotFound();
//     //        }
//     //        var events = await _context.Events.FindAsync(id);
//     //        if (events == null)
//     //        {
//     //            return NotFound();
//     //        }
//
//     //        _context.Events.Remove(events);
//     //        await _context.SaveChangesAsync();
//
//     //        return NoContent();
//     //    }
//
//     //    private bool EventsExists(int id)
//     //    {
//     //        return (_context.Events?.Any(e => e.event_id == id)).GetValueOrDefault();
//     //    }
//     }
// }
