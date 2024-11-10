
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagementSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        //[RegularExpression(@"^[A-Z]-[a-zA-Z]*$")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        [Column("FirstName")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrolment Date")]
        public DateTime EnrolmentDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ",  " + FirstMidName;
            }
        }
        public ICollection<Enrolment> Enrolments { get; set; }
    }
}
