using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniVerServer;
using UniVerServer.Models;
using UniVerServer.Models.CustomDataObjects;
using UniVerServer.Models.DTO;

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

            var data = await (from enrollment in _context.Courses
                                     join learner in _context.People
                                     on enrollment.student_id equals learner.person_system_identifier
                                     join subject in _context.Subjects
                                     on enrollment.Subjects equals subject.subject_id
                                     join lecturer in _context.People 
                                     on subject.lecturer_id equals lecturer.person_id
                                     join role in _context.Roles
                                     on lecturer.role equals role.role_id
                                     select new CourseEnrollmentView
                                     {
                                       student_id = learner.person_id,
                                       student_name = learner.first_name + " "+ learner.last_name,
                                       student_number = learner.person_cell,
                                       student_email = learner.person_email,
                                       student_credits = learner.person_credits,
                                       student_needed_credits = learner.needed_credits,

                                       lecturer_id = lecturer.person_id,
                                       lecturer_name = lecturer.first_name + " " + lecturer.last_name,
                                       lecturer_rate = role.rate,

                                       subject_id = subject.subject_id,
                                       subject_name = subject.subject_name,
                                       subject_code = subject.subject_code, 
                                       subject_cost = subject.subject_cost, 
                                       subject_color = subject.subject_color,   
                                       subject_credits = subject.subject_credits,
                                       subject_runtime = subject.subject_class_runtiem,
                                       class_amount = subject.subject_class_amount,
                                       subject_active = subject.is_active,
                                       subject_start = subject.course_start

                                     }
                                     ).ToListAsync();
          if (_context.Courses == null)
          {
              return NotFound();
          }
            return Ok(data);
        }
        [HttpGet("studentFees")]
        public async Task<ActionResult<CourseEnrollments>> GetAllLecturerFees()
        {
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var data = await (from enrollment in _context.Courses
                              join learner in _context.People
                              on enrollment.student_id equals learner.person_system_identifier
                              join subject in _context.Subjects
                              on enrollment.Subjects equals subject.subject_id
                              join lecturer in _context.People
                              on subject.lecturer_id equals lecturer.person_id
                              join role in _context.Roles
                              on lecturer.role equals role.role_id
                              select new CourseEnrollmentView
                              {
                                  student_id = learner.person_id,
                                  student_name = learner.first_name + " " + learner.last_name,
                                  student_number = learner.person_cell,
                                  student_email = learner.person_email,
                                  student_credits = learner.person_credits,
                                  student_needed_credits = learner.needed_credits,

                                  subject_id = subject.subject_id,
                                  subject_name = subject.subject_name,
                                  subject_cost = subject.subject_cost,
                                  subject_credits = subject.subject_credits,
                                  subject_runtime = subject.subject_class_runtiem,
                              
                              }
                         ).ToListAsync();


            var result = data
                            .GroupBy(item => item.student_name)
                            .Select(group => new
                            {
                                credits = group.ToList()[0].student_credits,
                                student = group.Key,
                                //courses = group.ToList(),
                                studentFee = group.Sum(item =>
                                {
                                    return item.subject_cost;
                                }),
                                aquiredCredits = group.Sum(item =>
                                {
                                    return item.student_credits;
                                })
                            });
            return Ok(result);
        }

        // GET: api/CourseEnrollments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseEnrollments>> GetCourseEnrollments(int id)
        {
          if (_context.Courses == null)
          {
              return NotFound();
          }
            var data = await (from enrollment in _context.Courses
                              join learner in _context.People
                              on enrollment.student_id equals learner.person_system_identifier
                              join subject in _context.Subjects
                              on enrollment.Subjects equals subject.subject_id
                              join lecturer in _context.People
                              on subject.lecturer_id equals lecturer.person_id
                              join role in _context.Roles
                              on lecturer.role equals role.role_id
                              select new CourseEnrollmentView
                              {
                                  student_id = learner.person_id,
                                  student_name = learner.first_name + " " + learner.last_name,
                                  student_number = learner.person_cell,
                                  student_email = learner.person_email,
                                  student_credits = learner.person_credits,
                                  student_needed_credits = learner.needed_credits,

                                  lecturer_id = lecturer.person_id,
                                  lecturer_name = lecturer.first_name + " " + lecturer.last_name,
                                  lecturer_rate = role.rate,

                                  subject_id = subject.subject_id,
                                  subject_name = subject.subject_name,
                                  subject_code = subject.subject_code,
                                  subject_cost = subject.subject_cost,
                                  subject_color = subject.subject_color,
                                  subject_credits = subject.subject_credits,
                                  subject_runtime = subject.subject_class_runtiem,
                                  class_amount = subject.subject_class_amount,
                                  subject_active = subject.is_active,
                                  subject_start = subject.course_start

                              }
                          ).FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
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
