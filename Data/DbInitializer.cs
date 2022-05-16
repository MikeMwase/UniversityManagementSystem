using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void initializer(VarsityContext context)
        {
            context.Database.EnsureCreated();

            //Look for any learners in the database

            if(context.learners.Any())
            {
                return;
            }

            var learners = new Learner[]
            {
               new Learner { FirstName = "Carson", LastName = "Alexander",
               IntakeDate = DateTime.Parse("2010-09-01") },
               new Learner { FirstName = "Meredith", LastName = "Alonso",
               IntakeDate = DateTime.Parse("2012-09-01") },
               new Learner { FirstName = "Arturo", LastName = "Anand",
               IntakeDate = DateTime.Parse("2013-09-01") },
              new Learner { FirstName = "Gytis", LastName = "Barzdukas",
              IntakeDate = DateTime.Parse("2012-09-01") },
              new Learner { FirstName = "Yan", LastName = "Li",
              IntakeDate = DateTime.Parse("2012-09-01") },
              new Learner { FirstName = "Peggy", LastName = "Justice",
              IntakeDate = DateTime.Parse("2011-09-01") },
              new Learner { FirstName = "Laura", LastName = "Norman",
              IntakeDate = DateTime.Parse("2013-09-01") },
              new Learner { FirstName = "Nino", LastName = "Olivetto",
              IntakeDate = DateTime.Parse("2005-09-01") }
            };
            foreach(Learner l in learners)
            {
                context.learners.Add(l);
            }
            context.SaveChanges();

            var lecturers = new Lecturer[]
            {
                new Lecturer { FirstMidName = "Kim", LastName = "Abercrombie",
                HireDate = DateTime.Parse("1995-03-11") },
                new Lecturer { FirstMidName = "Fadi", LastName = "Fakhouri",
                HireDate = DateTime.Parse("2002-07-06") },
                new Lecturer { FirstMidName = "Roger", LastName = "Harui",
                HireDate = DateTime.Parse("1998-07-01") },
                new Lecturer { FirstMidName = "Candace", LastName = "Kapoor",
                HireDate = DateTime.Parse("2001-01-15") },
                new Lecturer { FirstMidName = "Roger", LastName = "Zheng",
                HireDate = DateTime.Parse("2004-02-12") }
            };
            foreach (Lecturer l in lecturers)
            {
                context.lectures.Add(l);
            };
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "English", Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID = lecturers.Single( i => i.LastName == "Abercrombie").ID },
                new Department { Name = "Mathematics", Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID = lecturers.Single( i => i.LastName == "Fakhouri").ID },
                new Department { Name = "Engineering", Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID = lecturers.Single( i => i.LastName == "Harui").ID },
                new Department { Name = "Economics", Budget = 100000,
               StartDate = DateTime.Parse("2007-09-01"),
               InstructorID = lecturers.Single( i => i.LastName == "Kapoor").ID }
            };

            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();



            var courses = new Course[]
            {
                new Course {CourseID = 1050, Title = "Chemistry", Credits = 3,
                DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID
                },
                new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
                DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
                },
                new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
                DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
                },
                new Course {CourseID = 1045, Title = "Calculus", Credits = 4,
                DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Course {CourseID = 3141, Title = "Trigonometry", Credits = 4,
                DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
                },
                new Course {CourseID = 2021, Title = "Composition", Credits = 3,
                DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },
                new Course {CourseID = 2042, Title = "Literature", Credits = 4,
                DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
                },

            };
            foreach(Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var facultyAssignments = new FacultyAssignment[]
            {
               new FacultyAssignment {
               LecturerID = lecturers.Single( i => i.LastName == "Fakhouri").ID,
               Location = "Smith 17" },
               new FacultyAssignment {
               LecturerID = lecturers.Single( i => i.LastName == "Harui").ID, 
               Location = "Gowan 27" },
               new FacultyAssignment {
               LecturerID = lecturers.Single( i => i.LastName == "Kapoor").ID,
               Location = "Thompson 304" },
            };
            foreach (FacultyAssignment f in facultyAssignments)
            {
                context.OfficeAssignments.Add(f);
            }
            context.SaveChanges();

            var courseInstructors = new CourseAssignment[]
            {
                new CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                LecturerID = lecturers.Single(i => i.LastName == "Kapoor").ID
                },

                new CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                 LecturerID = lecturers.Single(i => i.LastName == "Harui").ID
                },

                new CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                 LecturerID = lecturers.Single(i => i.LastName == "Zheng").ID
                },

                new CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                 LecturerID = lecturers.Single(i => i.LastName == "Zheng").ID
                },

               new CourseAssignment {
               CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                LecturerID = lecturers.Single(i => i.LastName == "Fakhouri").ID
               },

               new CourseAssignment {
               CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
               LecturerID = lecturers.Single(i => i.LastName == "Harui").ID
               },

               new CourseAssignment {
               CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
               LecturerID = lecturers.Single(i => i.LastName == "Abercrombie").ID
               },


               new CourseAssignment {
               CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
               LecturerID = lecturers.Single(i => i.LastName == "Abercrombie").ID
               },
            };
            foreach (CourseAssignment ca in courseInstructors)
            {
                context.CourseAssignments.Add(ca);
            }
            context.SaveChanges();

            var intakes = new Intake[]
            {
                new Intake {
                LearnerID = learners.Single(s => s.LastName == "Alexander").ID,
                CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                Mark = Mark.A
                },

                new Intake {
                LearnerID = learners.Single(s => s.LastName == "Alexander").ID,
                CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                 Mark = Mark.C
                },

               new Intake {
               LearnerID = learners.Single(s => s.LastName == "Alexander").ID,
               CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
               Mark = Mark.B
               },

               new Intake {
               LearnerID = learners.Single(s => s.LastName == "Alonso").ID,
               CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
               Mark = Mark.B
               },

               new Intake {
               LearnerID = learners.Single(s => s.LastName == "Alonso").ID,
               CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
               Mark = Mark.B
               },
            };
            foreach (Intake i in intakes)
            {
                var enrollmentInDataBase = context.intakes.Where(
                s =>
                s.LearnerID == i.LearnerID &&
                s.Courses.CourseID == i.CourseID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.intakes.Add(i);
                }
            }
            context.SaveChanges();
        }
    }
}
