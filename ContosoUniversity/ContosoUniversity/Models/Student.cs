using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Student
    {

        /// <summary>
        /// Primary Key de la tabla
        /// </summary>
        [Key]
        [Required(ErrorMessage = "El campo Id es obligatorio")]
        public int Id { get; set; }

        /// <summary>
        /// Apellidos del Estudiante
        /// </summary>
        [Required(ErrorMessage = "El campo LastName es obligatorio")]
        [StringLength(30, ErrorMessage = "La maxima longitud del campo LastName es de 30 caracteres")]
        [DataType(DataType.Text, ErrorMessage = "El campo LastName solo acepta caracteres")]
        public string LastName { get; set; }

        /// <summary>
        /// Nombre del Estudiante
        /// </summary>
        [Required(ErrorMessage = "El campo FirstName es obligatorio")]
        [StringLength(30, ErrorMessage = "La maxima longitud del campo FirstName es de 30 Caracteres")]
        [DataType(DataType.Text, ErrorMessage = "El campo FirstName solo acepta caracteres")]
        public string FirstName { get; set; }

        /// <summary>
        /// Fecha de enrolamiento
        /// </summary>
        [Required(ErrorMessage = "El campo EnrollmentDate es obligatorio")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

        /// <summary>
        /// Relacion entre la clase Enrollment - <see cref="Student"/>
        /// </summary>
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}