using System.Collections.Generic;
using System.Data;
using System.Net;
using System;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Events.Models;
using UniVerServer.Models;
using UniVerServer.Subjects.Models;

namespace UniVerServer
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
     
        
   
        // public DbSet<MadePayments> MadePayments { get; set; }
        // public DbSet<OutStandingStudentFees> OutStandingStudentFees { get; set; }
        //
       
        // public DbSet<UniVerServer.Models.PaymentSummary> PaymentSummary { get; set; } = default!;
        // // Redone tables
        public DbSet<Users.Models.Users> Users { get; set; }
        public DbSet<Roles.Models.Roles> Roles { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Event> Events { get; set; }
        // public DbSet<Events> Events { get; set; }
        // public DbSet<Enrollment> Enrollments { get; set; }
        // public DbSet<StudentCourses> StudentCourses { get; set; }
        // public DbSet<CourseEnrollments> Courses { get; set; }
    }

}
