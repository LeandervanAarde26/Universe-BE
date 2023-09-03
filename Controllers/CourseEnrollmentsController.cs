using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<ActionResult<IEnumerable<SubjectWithEnrollments>>> GetCourses()
        {
            var data = await (from subject in _context.Subjects
                              join lecturer in _context.People
                              on subject.lecturer_id equals lecturer.person_id
                              join role in _context.Roles
                              on lecturer.role equals role.role_id
                              join enrollment in _context.Courses
                              on subject.subject_id equals enrollment.Subjects into enrollmentsGroup
                              from enrollment in enrollmentsGroup.DefaultIfEmpty()
                              join learner in _context.People
                              on enrollment.student_id equals learner.person_system_identifier into learnersGroup
                              from learner in learnersGroup.DefaultIfEmpty()
                              select new CourseEnrollmentView
                              {
                                  student_id = learner != null ? learner.person_id : 0,
                                  student_name = learner != null ? learner.first_name + " " + learner.last_name : null,
                                  student_number = learner != null ? learner.person_cell : null,
                                  student_email = learner != null ? learner.person_email : null,
                                  student_credits = learner != null ? learner.person_credits : 0,
                                  student_needed_credits = learner != null ? learner.needed_credits : 0,
                                  subject_description = subject.subject_description,
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
                            )
                            .GroupBy(e => e.subject_name)
                            .Select(group => new SubjectWithEnrollments
                            {
                                subjectName = group.Key,
                                subjectDescription = group.First().subject_description,
                                lecturer_id = group.First().lecturer_id,
                                lecturer_name = group.First().lecturer_name,
                                subjectId = group.First().subject_id,
                                subject_code = group.First().subject_code,
                                subject_color = group.First().subject_color,
                                subject_active = group.First().subject_active,
                                enrollments = group.Where(e => e.student_id != null).Select(e => new Enrollment
                                {
                                    student_id = e.student_id,
                                    student_name = e.student_name,
                                    student_email = e.student_email,
                                }).ToList()
                            })
                            .ToListAsync();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }


        [HttpGet("subject/{id}")]
        public async Task<ActionResult<SubjectWithEnrollments>> GetSingleSubjectOverView(int id)
        {
            if(_context.Courses == null)
            {
                return NotFound();
            }
            var subjectWithEnrollments = await (from enrollment in _context.Courses
                                                join learner in _context.People
                                                on enrollment.student_id equals learner.person_system_identifier
                                                join subject in _context.Subjects
                                                on enrollment.Subjects equals subject.subject_id
                                                join lecturer in _context.People
                                                on subject.lecturer_id equals lecturer.person_id
                                                where enrollment.Subjects == id
                                                select new SingleSubjectView
                                                {
                                                    student_id = learner.person_id,
                                                    student_name = learner.first_name,
                                                    student_email = learner.person_email,
                                                    lecturer_id = lecturer.person_id,
                                                    lecturer_name = lecturer.first_name + " " + lecturer.last_name,
                                                    subject_id = subject.subject_id,
                                                    subject_name = subject.subject_name,  
                                                    subject_color = subject.subject_color,
                                                    subject_code = subject.subject_code,
                                                    subject_active = subject.is_active,
                                                    subject_description = subject.subject_description,
                                                    enrollment_id = enrollment.enrollment_id
                                                })
                                               .ToListAsync();

            var groupedData = subjectWithEnrollments
                .GroupBy(e => e.subject_name)
                .Select(group => new SubjectWithEnrollments
                {
                    subjectName = group.Key,
                    subjectDescription = group.First().subject_description,
                    lecturer_id = group.First().lecturer_id,
                    lecturer_name = group.First().lecturer_name,
                    subjectId = group.First().subject_id,
                    subject_code = group.First().subject_code,
                    subject_color = group.First().subject_color,
                    subject_active = group.First().subject_active,
                    enrollments = group.Select(e => new Enrollment
                    {
                        student_id = e.student_id,
                        student_name = e.student_name,
                        student_email = e.student_email,
                        enrollment_id=e.enrollment_id,
                    }).ToList()
                })
                .FirstOrDefault();
            return Ok(groupedData);
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
                                  student_systemIdentifier = learner.person_system_identifier,
                                  student_number = learner.person_cell,
                                  student_email = learner.person_email,
                                  student_credits = learner.person_credits,
                                  student_needed_credits = learner.needed_credits,
                                  subject_description = subject.subject_description,

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

                                student = group.Key,
                                studentId = group.ToList()[0].student_id,
                                studentNumber = group.ToList()[0].student_systemIdentifier,
                                credits = group.ToList()[0].student_credits,
                                //courses = group.ToList(),
                                studentYearlyFee = group.Sum(item =>
                                {
                                    return item.subject_cost;
                                }),
                                studentMonthlyFee = group.Sum(item =>
                                {
                                    return item.subject_cost;
                                }
                                ) / 12 ,
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
            //var person = await _context.People.Where(p => p.person_system_identifier.Equals(id)).FirstOrDefaultAsync();

            var studentToBeRemoved = await _context.Courses.Where(p => p.enrollment_id.Equals(id)).FirstOrDefaultAsync();


            if (courseEnrollments == null || studentToBeRemoved == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(studentToBeRemoved);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseEnrollmentsExists(int id)
        {
            return (_context.Courses?.Any(e => e.enrollment_id == id)).GetValueOrDefault();
        }
    }
}
