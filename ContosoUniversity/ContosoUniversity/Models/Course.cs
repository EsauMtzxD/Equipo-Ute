﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Course
    {

        /// <summary>
        /// Primary Key de la tabla
        /// </summary>
        [Key]
        [Required(ErrorMessage = "El campo Id es obligatorio")]
        public int Id { get; set; }

        /// <summary>
        /// Titulo del curso
        /// </summary>
        [Required(ErrorMessage = "El campo Title es obligatorio")]
        [StringLength(30, ErrorMessage = "La maxima longitud del campo es de 30 caracteres")]
        [DataType(DataType.Text, ErrorMessage = "El campo solo acepta caracteres")]
        public string Tittle { get; set; }

        /// <summary>
        /// Creditos que necesita el curso
        /// </summary>
        [Required(ErrorMessage = "El campo Credits es obligatorio")]
        public int Credits { get; set; }

        [ForeignKey("Departament")]
        public int DepartamentId { get; set; }
        public Department Departament { get; set; }

        /// <summary>
        /// Relacion entre la tabla Enrollment - <see cref="Course"/>
        /// </summary>
        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}