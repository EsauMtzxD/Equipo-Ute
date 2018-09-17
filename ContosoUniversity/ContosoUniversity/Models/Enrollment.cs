using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Enrollment
    {

        /// <summary>
        /// Primary Key de la tabla
        /// </summary>
        [Key]
        [Required(ErrorMessage = "El campo Id es obligatorio")]
        public int Id { get; set; }

        /// <summary>
        /// Foreign Key de la tabla Course
        /// </summary>
        [ForeignKey("Course")]
        [Required(ErrorMessage = "El campo CourseId es obligatorio")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        /// <summary>
        /// Foreign Key de la tabla Student
        /// </summary>
        [ForeignKey("Student")]
        [Required(ErrorMessage = "El campo StudentId es obligatorio")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        /// <summary>
        /// El grado en el que se encuentra el estudiante
        /// </summary>
        [Required(ErrorMessage = "El campo Grade es obligatorio")]
        [DataType(DataType.Text)]
        [StringLength(15, ErrorMessage = "La maxima longitud del campo es de 10 caracteres")]
        public string Grade { get; set; }

    }
}