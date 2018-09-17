using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Instructor
    {

        [Key]
        [Required(ErrorMessage = "El campo Id es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo LastName es obligatorio")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo FirstMidName es obligatorio")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "El campo HireDate es obligatorio")]
        public DateTime HireDate { get; set; }

        ICollection<Department> Departments { get; set; }
        ICollection<OfficeAssignment> OfficeAssignments { get; set; }

    }
}