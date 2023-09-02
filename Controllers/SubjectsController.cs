using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using UniVerServer;
using UniVerServer.Models;
using UniVerServer.Models.CustomDataObjects;
using UniVerServer.Models.DTO;

namespace UniVerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectsWithLecturers>>> GetSubjects()
        {
            var subjectList = await (from subject in _context.Subjects
                                     join lecturer in _context.People
                                     on subject.lecturer_id
                                     equals lecturer.person_id
                                     select new SubjectsWithLecturers
                                     {
                                         subject = subject,
                                         lecturerName = lecturer.first_name + " " + lecturer.last_name
                                     })
                                     .ToListAsync();

            if (subjectList == null || subjectList.Count == 0)
            {
                return NotFound();
            }

            return Ok(subjectList);
        }

        [HttpGet("lecturerfees")]
        public async Task<ActionResult<CourseEnrollments>> GetAllLecturerFees()
        {
            if (_context.Subjects == null)
            {
                return NotFound();
            }
     
            var lecturersPayment = await (from subject in _context.Subjects
                                     join lecturer in _context.People
                                     on subject.lecturer_id
                                     equals lecturer.person_id
                                     join role in _context.Roles
                                     on lecturer.role equals role.role_id                      
                                     select new LecturerPayment
                                     {
                                        subject_id = subject.subject_id,
                                        subject_name = subject.subject_name,
                                        lecturer_id = lecturer.person_id,
                                        lecturer = lecturer.first_name +" " + lecturer.last_name,
                                        monthlyIncome = Math.Round(subject.subject_class_amount * (((decimal)subject.course_start.Day / new DateTime(subject.course_start.Year, subject.course_start.Month, DateTime.DaysInMonth(subject.course_start.Year, subject.course_start.Month)).Day) * role.rate), 2),
                                        hoursWorked = Math.Round(subject.subject_class_amount * (((decimal)subject.course_start.Day / new DateTime(subject.course_start.Year, subject.course_start.Month, DateTime.DaysInMonth(subject.course_start.Year, subject.course_start.Month)).Day)))
                                     })
                                      .ToListAsync();

                var result = lecturersPayment
                             .GroupBy(item => item.lecturer)
                             .Select(group => new
                              {
                                lecturer = group.Key,
                                //subjects = group.ToList(),
                                totalHoursWorked = group.Sum(item =>
                                {
                                    return item.hoursWorked;
                                }),
                                monthlyIncome = group.Sum(item =>
                                {
                                    return item.monthlyIncome;
                                })
                            })
                             .ToList();

            return Ok(result);
        }


        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subjects>> GetSubjects(int id)
        {
          if (_context.Subjects == null)
          {
              return NotFound();
          }
            var subjects = await _context.Subjects.FindAsync(id);

            if (subjects == null)
            {
                return NotFound();
            }

            return subjects;
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjects(int id, Subjects subjects)
        {
            if (id != subjects.subject_id)
            {
                return BadRequest();
            }

            _context.Entry(subjects).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectsExists(id))
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

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Subjects>> PostSubjects(Subjects subjects)
        {
          

            if (_context.Subjects == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Subjects'  is null.");
          }
           
            _context.Subjects.Add(subjects);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubjects", new { id = subjects.subject_id }, subjects);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjects(int id)
        {
            if (_context.Subjects == null)
            {
                return NotFound();
            }
            var subjects = await _context.Subjects.FindAsync(id);
            if (subjects == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subjects);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectsExists(int id)
        {
            return (_context.Subjects?.Any(e => e.subject_id == id)).GetValueOrDefault();
        }
    }
}
