using ContosoUniversity.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class StudentController : Controller
    {

        private ContosoUniversityContext dbCtx = new ContosoUniversityContext();

        #region Index and Details

        public ActionResult Index()
        {

            List<Student> students = new List<Student>();
            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            MySqlDataReader read;

            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using(MySqlCommand cmd = new MySqlCommand("ViewStudent", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        Student s = new Student();
                        s.Id = Convert.ToInt32(read["Id"]);
                        s.LastName = read["LastName"].ToString();
                        s.FirstName = read["FirstName"].ToString();
                        s.EnrollmentDate = Convert.ToDateTime(read["EnrollmentDate"]);

                        students.Add(s);

                    }
                    if(read == null)
                    {

                        return HttpNotFound();

                    }

                }

                conn.Close();

            }

            return View(students);
        }

        public ActionResult Details(int id)
        {

            Student s = new Student();
            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            MySqlDataReader read;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("_ViewStudent", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_Id", id);

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        s.LastName = read["LastName"].ToString();
                        s.FirstName = read["FirstName"].ToString();
                        s.EnrollmentDate = Convert.ToDateTime(read["EnrollmentDate"]);

                    }
                    if(read == null)
                    {

                        return HttpNotFound();

                    }

                }

            }

            return View(s);
        }
        #endregion

        #region Create camp

        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(Student s)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
                        using(MySqlConnection conn = new MySqlConnection(ConnectionString))
                        {

                            conn.Open();

                            using(MySqlCommand cmd = new MySqlCommand("InsertStudent", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@_Last", s.LastName);
                                cmd.Parameters.AddWithValue("@_First", s.FirstName);
                                cmd.Parameters.AddWithValue("@_Enrollment", s.EnrollmentDate);

                                cmd.ExecuteNonQuery();

                            }

                            conn.Close();
                        }

                    }//End Second Try
                    catch (DbEntityValidationException e)
                    {

                        foreach (var eve in e.EntityValidationErrors)
                        {

                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: ",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);

                            foreach (var ve in eve.ValidationErrors)
                            {

                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);

                            }//End Second foreach

                        }//End first foreach
                        throw;

                    }//End Catch

                }//End the IF

                return RedirectToAction("Index");
            }//End Secod Try
            catch(Exception e)
            {

                Console.WriteLine(e);

                return View();
            }//End Catch
        }
        #endregion

        #region Edit the camp
        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {

            Student student = dbCtx.Students.Find(id);

            if(student == null)
            {

                return HttpNotFound();

            }

            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        public ActionResult Edit(Student student, int id)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
                        MySqlDataReader read;
                        using(MySqlConnection conn = new MySqlConnection(ConnectionString))
                        {

                            conn.Open();

                            using(MySqlCommand cmd = new MySqlCommand("EditStudent", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@_Last", student.LastName);
                                cmd.Parameters.AddWithValue("@_First", student.FirstName);
                                cmd.Parameters.AddWithValue("@_Enrollment", student.EnrollmentDate);
                                cmd.Parameters.AddWithValue("@_Id", id);

                                read = cmd.ExecuteReader();

                                while (read.Read())
                                {

                                    student.LastName = read["LastName"].ToString();
                                    student.FirstName = read["FirstMidName"].ToString();
                                    student.EnrollmentDate = Convert.ToDateTime(read["EnrollmentDate"]);

                                }
                                if (read == null)
                                {

                                    return HttpNotFound();

                                }

                            }

                            conn.Close();

                        }

                    }//End secod try
                    catch (DbEntityValidationException e)
                    {

                        foreach (var eve in e.EntityValidationErrors)
                        {

                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: ",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);

                            foreach (var ve in eve.ValidationErrors)
                            {

                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);

                            }//End second foreach

                        }//End Firts foreach
                        throw;

                    }//End firts Catch

                }//End IF

                return RedirectToAction("Index");
            }//End firts try
            catch(Exception e)
            {

                Console.WriteLine(e);

                return View();
            }//End second Catch
        }//End Method
        #endregion

        #region Delete the camp
        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            Student student = dbCtx.Students.Find(id);

            if(student == null)
            {

                return HttpNotFound();

            }

            return View(student);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {

                try
                {

                    Student s = new Student();
                    string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
                    MySqlDataReader read;
                    using(MySqlConnection conn = new MySqlConnection(ConnectionString))
                    {

                        conn.Open();

                        using(MySqlCommand cmd = new MySqlCommand("DeleteStudent", conn))
                        {

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@_Id", id);

                            read = cmd.ExecuteReader();

                            while (read.Read())
                            {

                                s.LastName = read["LastName"].ToString();
                                s.FirstName = read["FirstMidName"].ToString();
                                s.EnrollmentDate = Convert.ToDateTime(read["EnrollmentDate"]);

                            }
                            if (read == null)
                            {

                                return HttpNotFound();

                            }

                        }


                        conn.Close();
                    }

                }
                catch (DbEntityValidationException e)
                {

                    foreach (var eve in e.EntityValidationErrors)
                    {

                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors: ",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);

                        foreach (var ve in eve.ValidationErrors)
                        {

                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);

                        }

                    }
                    throw;

                }

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {

                Console.WriteLine(e);

                return View();
            }
        }
        #endregion

    }
}
