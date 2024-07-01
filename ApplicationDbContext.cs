using System.Collections.Generic;
using System.Data;
using System.Net;
using System;
using Microsoft.EntityFrameworkCore;
using UniVerServer.Courses.Models;
using UniVerServer.Courses.Seed;
using UniVerServer.Enrollments.Models;
using UniVerServer.Enrollments.Seed;
using UniVerServer.Events.Models;
using UniVerServer.Events.Seed;
using UniVerServer.Models;
using UniVerServer.Roles.Seed;
using UniVerServer.Seed;
using UniVerServer.Subjects.Models;
using UniVerServer.Subjects.Seed;

namespace UniVerServer
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        // public DbSet<MadePayments> MadePayments { get; set; }
        // public DbSet<OutStandingStudentFees> OutStandingStudentFees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleSeed());
            modelBuilder.ApplyConfiguration(new UserSeeding());
            modelBuilder.ApplyConfiguration(new SubjectSeed());
            modelBuilder.ApplyConfiguration(new EventSeed());
            modelBuilder.ApplyConfiguration(new CourseSeed());
            modelBuilder.ApplyConfiguration(new EnrollmentSeed());
        }
        
        // public DbSet<UniVerServer.Models.PaymentSummary> PaymentSummary { get; set; } = default!;
        // // Redone tables
        public DbSet<Users.Models.Users> Users { get; set; }
        public DbSet<Roles.Models.Roles> Roles { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Event> Events { get; set; }
        // public DbSet<Events> Events { get; set; }
        public DbSet<Course> Courses { get; set; }
        // public DbSet<StudentCourses> StudentCourses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        // public DbSet<CourseEnrollments> Courses { get; set; }
    }

}
