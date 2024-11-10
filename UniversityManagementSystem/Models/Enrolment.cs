using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models
{
    public enum Grade
    {
        A, B, C, D, E, F
    }

    public class Enrolment
    {
        public int EnrolmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade Grade { get; set; }
        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
