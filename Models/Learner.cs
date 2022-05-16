using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityManagementSystem.Models
{
    public class Learner
    {
        public int ID { get; set; }
        [Required]
        [Display(Name =("Last Name"))]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name ="First Name")]
        [StringLength(50)]      
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime IntakeDate { get; set; }

        public ICollection<Intake> intakes { get; set; }

        [Display(Name ="Full Name")]

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

    }
}
