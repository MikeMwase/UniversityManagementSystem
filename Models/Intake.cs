using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagementSystem.Models
{
    public enum Mark
    {
        A,B,C,D,E,F
    }
    public class Intake
    {
        public int IntakeId { get; set; }
        public int CourseID { get; set; }
        public int LearnerID { get; set; }

        [DisplayFormat(NullDisplayText = "No Mark")]
        public Mark? Mark { get; set; }
        public Course Courses { get; set; }
        public Learner Learner { get; set; }

    }
}
