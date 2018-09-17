using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Department
    {

        [Key]
        [Required(ErrorMessage = "El campo Id es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Name es obligatorio")]
        [StringLength(25, ErrorMessage = "La maxima longitud del campo es de 25 caracteres")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo Budge es obligatorio")]
        public double Budget { get; set; }

        [Required(ErrorMessage = "El campo StartDate es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [ForeignKey("Instructor")]
        [Required(ErrorMessage = "El campo InstructorId es obligatorio")]
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

    }
}