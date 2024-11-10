using System;
using System.ComponentModel.DataAnnotations;

namespace UniversityManagementSystem.Models.UniversityViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? EnrolmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}
