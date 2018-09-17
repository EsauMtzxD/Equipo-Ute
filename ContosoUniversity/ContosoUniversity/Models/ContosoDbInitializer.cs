using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{

    public class ContosoDbInitializer : DropCreateDatabaseAlways<ContosoUniversityContext>
    {

        /// <summary>
        /// Metodo para insertar datos a la base de datos por default
        /// </summary>
        /// <param name="dbCtx"></param>
        protected override void Seed(ContosoUniversityContext dbCtx)
        {


            List<Student> students = new List<Student>();
            students.Add(new Student()
            {
                LastName = "Mtz",
                FirstName = "Esau",
                EnrollmentDate = DateTime.Now

            });

            students.Add(new Student()
            {

                LastName = "Garcia",
                FirstName = "Abby",
                EnrollmentDate = DateTime.Now

            });

            students.Add(new Student()
            {

                LastName = "Erasto",
                FirstName = "Sarahi",
                EnrollmentDate = DateTime.Now

            });

            dbCtx.Students.AddRange(students);

            dbCtx.SaveChanges();

            List<Course> courses = new List<Course>();
            courses.Add(new Course()
            {

                Tittle = "Base de Datos",
                Credits = 5,

            });

            courses.Add(new Course()
            {

                Tittle = "Programacion",
                Credits = 5,

            });

            courses.Add(new Course()
            {

                Tittle = "Redes",
                Credits = 4,

            });

            dbCtx.Courses.AddRange(courses);

            dbCtx.SaveChanges();

            List<Enrollment> enrollments = new List<Enrollment>();
            enrollments.Add(new Enrollment()
            {
                CourseId = 1,
                StudentId = 1,
                Grade = "Cuarto"

            });

            enrollments.Add(new Enrollment()
            {
                CourseId = 2,
                StudentId = 2,
                Grade = "Cuarto"

            });

            enrollments.Add(new Enrollment()
            {
                CourseId = 3,
                StudentId = 3,
                Grade = "Cuarto"

            });

            dbCtx.Enrollments.AddRange(enrollments);

            dbCtx.SaveChanges();


            base.Seed(dbCtx);

        }

    }

}
