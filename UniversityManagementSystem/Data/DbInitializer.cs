using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Data
{
    public static class DbInitializer
    {
       public static void Initializer(UniversityContext context)
       {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var student = new Student[]
            {
                new Student{ LastName = "Mwase", FirstMidName = "Mike", EnrolmentDate = DateTime.Parse("1991/07/01")},
                new Student{ LastName = "Mwase", FirstMidName = "Snethemba", EnrolmentDate = DateTime.Parse("2022/04/28")},
                new Student{ LastName = "Nkabinde", FirstMidName = "Wisdom", EnrolmentDate = DateTime.Parse("1993/02/11")},
                new Student{ LastName = "Ngwepe", FirstMidName = "Johannes", EnrolmentDate = DateTime.Parse("1987/07/31")},
                new Student{ LastName = "Ngwepe", FirstMidName = "Tony", EnrolmentDate = DateTime.Parse("1991/07/01")},
                new Student{ LastName = "Nqoko", FirstMidName = "Uyanda", EnrolmentDate = DateTime.Parse("1984/12/01")}
            };
            foreach(Student s in student)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor{ LastName = "Mwase", FirstMidName = "Dimox", HireDate = DateTime.Parse("1970/09/27")},
                new Instructor{ LastName = "Ndabeni", FirstMidName = "Joseph", HireDate = DateTime.Parse("1981/12/31")},
                new Instructor{ LastName = "Nqoko", FirstMidName = "Arnet", HireDate = DateTime.Parse("1966/11/11") },                
                new Instructor{ LastName = "Mwase", FirstMidName = "Musa", HireDate = DateTime.Parse("2021/08/07")},
                new Instructor{ LastName = "Mwase", FirstMidName = "Nancy", HireDate = DateTime.Parse("1999/03/08")}
            };
            foreach(Instructor i in instructors)
            {
                context.Instructors.Add(i);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department{Name = "English", Budget = 350000, StartDate = DateTime.Parse("2007/09/01"), InstructorID = instructors.Single(i => i.FirstMidName == "Arent").InstructorId},
                new Department{Name = "Mathematics", Budget = 10000, StartDate = DateTime.Parse("2007/09/01"), InstructorID = instructors.Single(i => i.FirstMidName == "Dimox").InstructorId},
                new Department{Name = "Economics", Budget = 350000, StartDate = DateTime.Parse("2007/09/01"), InstructorID = instructors.Single(i => i.FirstMidName == "Musa").InstructorId},
                new Department{Name = "Engineering", Budget = 350000, StartDate = DateTime.Parse("2007/09/01"), InstructorID = instructors.Single(i => i.FirstMidName == "Joseph").InstructorId},
            };
            foreach(Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID=1050, Title="Chemistry", Credits=3, DepartmentId = departments.Single(e => e.Name == "Engineering").DepartmentId},
                new Course{CourseID=4022, Title="Microeconomics", Credits = 3, DepartmentId = departments.Single(e => e.Name == "Economics").DepartmentId},
                new Course{CourseID=4041, Title="Macroeconomics", Credits = 3, DepartmentId=departments.Single(e => e.Name == "Economics").DepartmentId},
                new Course{CourseID=1045, Title="Calculus", Credits = 4, DepartmentId = departments.Single(m => m.Name == "Mathematics").DepartmentId},
                new Course{CourseID=3141, Title="Trigonometry", Credits = 4, DepartmentId = departments.Single(m => m.Name == "Mathematics").DepartmentId},
                new Course{CourseID=2021, Title="Composition", Credits = 3, DepartmentId = departments.Single(m => m.Name == "English").DepartmentId},
                new Course{CourseID=2042, Title="Literature", Credits = 4, DepartmentId = departments.Single(m => m.Name == "English").DepartmentId}
            };
            foreach(Course c in courses) 
            {
               context.Courses.Add(c);
            }
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
               new OfficeAssignment{instructorId = instructors.Single(i => i.FirstMidName == "Dimox").InstructorId, Location = "Smith 17"},
               new OfficeAssignment{instructorId = instructors.Single(i => i.FirstMidName == "Arent").InstructorId, Location = "Gowan 27"},
               new OfficeAssignment{instructorId = instructors.Single(i => i.FirstMidName == "Joseph").InstructorId, Location = "Thompson 304"}
            };
            foreach(OfficeAssignment o in officeAssignments)
            {
                context.OfficeAssignments.Add(o);
            }
            context.SaveChanges();

            var courseInstructor = new CourseAssignment[]
            {
                new CourseAssignment{CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, instructorID = instructors.Single(i => i.FirstMidName == "Joseph").InstructorId},
                new CourseAssignment{CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, instructorID = instructors.Single(i => i.FirstMidName == "Nancy").InstructorId},
                new CourseAssignment{CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID, instructorID = instructors.Single(i => i.FirstMidName == "Musa").InstructorId},
                new CourseAssignment{CourseID = courses.Single(c => c.Title == "Macroeconomics").CourseID, instructorID = instructors.Single(i => i.FirstMidName == "Musa").InstructorId},
                new CourseAssignment{CourseID = courses.Single(c => c.Title == "Calculus").CourseID, instructorID = instructors.Single(i => i.FirstMidName == "Dimox").InstructorId},
                new CourseAssignment{CourseID = courses.Single(c => c.Title == "Trigonometry").CourseID, instructorID =instructors.Single(i => i.FirstMidName == "Nancy").InstructorId},
                new CourseAssignment{CourseID = courses.Single(c => c.Title == "Composition").CourseID, instructorID=instructors.Single(i => i.FirstMidName == "Arnet").InstructorId},
                new CourseAssignment{CourseID = courses.Single(c => c.Title == "Literature").CourseID, instructorID = instructors.Single(i => i.FirstMidName == "Arnet").InstructorId }
            };
            foreach(CourseAssignment ci in courseInstructor)
            {
                context.CourseAssignments.Add(ci);
            }
            context.SaveChanges();


            var enrolment = new Enrolment[]
            {
                // new Enrolment{StudentID=1,CourseID=1050,Grade=Grade.A}
               
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Mike").StudentId, CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, Grade = Grade.A},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Mike").StudentId, CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID, Grade = Grade.C},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Mike").StudentId, CourseID = courses.Single(c => c.Title == "Macroeconomics").CourseID, Grade = Grade.B},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Snethemba").StudentId, CourseID = courses.Single(c => c.Title == "Calculus").CourseID, Grade = Grade.A},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Snethemba").StudentId, CourseID = courses.Single(c => c.Title == "Trigonometry").CourseID, Grade = Grade.A},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Snethemba").StudentId, CourseID = courses.Single(c => c.Title == "Composition").CourseID, Grade = Grade.A},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Wisdom").StudentId, CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, Grade = Grade.A},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Wisdom").StudentId, CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID, Grade = Grade.A},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Johannes").StudentId, CourseID = courses.Single(c => c.Title == "Chemistry").CourseID, Grade = Grade.B},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Uyanda").StudentId, CourseID = courses.Single(c => c.Title == "Composition").CourseID, Grade = Grade.B},
                new Enrolment{StudentID = student.Single(s => s.FirstMidName == "Tony").StudentId, CourseID = courses.Single(c => c.Title == "Literature").CourseID, Grade = Grade.B}
            };
            foreach(Enrolment e in enrolment)
            {
                var enrolmentInDbContext = context.Enrolments.Where(s => s.StudentID == e.StudentID && s.CourseID == e.CourseID).SingleOrDefault();

                if(enrolmentInDbContext == null)
                {
                    context.Enrolments.Add(e);
                }
                
            }
            context.SaveChanges();
        }
    }
}
