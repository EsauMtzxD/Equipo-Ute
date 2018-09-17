using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class OfficeAssignment
    {

        [Key]
        [Required(ErrorMessage = "El campo ID es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Location es obligatorio")]
        public string Location { get; set; }

        [ForeignKey("Instructor")]
        [Required(ErrorMessage = "El campo InstructorId es obligatorio")]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }


    }
}