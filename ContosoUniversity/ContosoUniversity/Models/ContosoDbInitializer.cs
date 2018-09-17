using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{

    public class ContosoDbInitializer : DropCreateDatabaseIfModelChanges<ContosoUniversityContext>
    {

        /// <summary>
        /// Metodo para insertar datos a la base de datos por default
        /// </summary>
        /// <param name="dbCtx"></param>
        protected override void Seed(ContosoUniversityContext dbCtx)
        {

            try
            {

                #region Instructor List
                List<Instructor> instructors = new List<Instructor>();
                instructors.Add(new Instructor()
                {

                    LastName = "Rodriguez",
                    FirstMidName = "Antonio",
                    HireDate = DateTime.Now

                });

                instructors.Add(new Instructor()
                {

                    LastName = "Martinez",
                    FirstMidName = "Jose",
                    HireDate = DateTime.Now

                });

                instructors.Add(new Instructor()
                {

                    LastName = "Estrada",
                    FirstMidName = "Juan",
                    HireDate = DateTime.Now

                });

                dbCtx.Instructors.AddRange(instructors);
                dbCtx.SaveChanges();

                #endregion

                #region Departament List
                List<Department> departments = new List<Department>();
                departments.Add(new Department()
                {

                    Name = "Sistemas",
                    Budget = 150000.0,
                    StartDate = DateTime.Now,
                    InstructorId = 1

                });

                departments.Add(new Department()
                {

                    Name = "Mecatronica",
                    Budget = 150000.0,
                    StartDate = DateTime.Now,
                    InstructorId = 2

                });

                departments.Add(new Department()
                {

                    Name = "Quimica",
                    Budget = 150000.0,
                    StartDate = DateTime.Now,
                    InstructorId = 3
              
                });

                dbCtx.Departments.AddRange(departments);
                dbCtx.SaveChanges();
                #endregion

                #region OfficeAssignment List
                List<OfficeAssignment> officeAssignments = new List<OfficeAssignment>();
                officeAssignments.Add(new OfficeAssignment()
                {

                    InstructorId = 1,
                    Location = "A lado de Direccion"

                });

                officeAssignments.Add(new OfficeAssignment()
                {

                    InstructorId = 2,
                    Location = "A lado de Direccion"

                });

                officeAssignments.Add(new OfficeAssignment()
                {

                    InstructorId = 3,
                    Location = "A lado de Direccion"

                });

                dbCtx.OfficeAssignments.AddRange(officeAssignments);
                dbCtx.SaveChanges();

                #endregion

                #region List - Student
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
                #endregion

                #region List - Courses
                List<Course> courses = new List<Course>();
                courses.Add(new Course()
                {

                    Tittle = "Base de Datos",
                    Credits = 5,
                    DepartamentId = 1

                });

                courses.Add(new Course()
                {

                    Tittle = "Programacion",
                    Credits = 5,
                    DepartamentId = 2

                });

                courses.Add(new Course()
                {

                    Tittle = "Redes",
                    Credits = 4,
                    DepartamentId = 3

                });

                dbCtx.Courses.AddRange(courses);
                dbCtx.SaveChanges();

                #endregion

                #region List - Enrollments

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
                #endregion

            }
            #region Catch
            catch (DbEntityValidationException e)
            {

                foreach(var eve in e.EntityValidationErrors)
                {

                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: ",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach(var ve in eve.ValidationErrors)
                    {

                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);

                    }

                }
                throw;

            }
            #endregion

            base.Seed(dbCtx);

        }

    }

}
