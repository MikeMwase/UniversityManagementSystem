using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Data
{
    public class VarsityContext : DbContext
    {
        public VarsityContext(DbContextOptions<VarsityContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses { get; set; }        
        public DbSet<Intake> intakes { get; set; }
        public DbSet<Learner> learners { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Lecturer> lectures { get; set; }
        public DbSet<FacultyAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Intake>().ToTable("Enrollment");
            modelBuilder.Entity<Learner>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Lecturer>().ToTable("Instructor");
            modelBuilder.Entity<FacultyAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");
            modelBuilder.Entity<CourseAssignment>()
            .HasKey(c => new { c.CourseID, c.LecturerID });
        }
    }
}
