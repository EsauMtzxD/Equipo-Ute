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
    public class EnrollmentController : Controller
    {

        private ContosoUniversityContext dbCtx = new ContosoUniversityContext();
        string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";


        #region Index and Details
        public ActionResult Index()
        {

            List<Enrollment> enrollments = new List<Enrollment>();
            MySqlDataReader read;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using(MySqlCommand cmd  = new MySqlCommand("ViewEnrollment", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        Enrollment enrollment = new Enrollment();
                        enrollment.Id = Convert.ToInt32(read["Id"]);
                        enrollment.CourseId = Convert.ToInt32(read["CourseId"]);
                        enrollment.StudentId = Convert.ToInt32(read["StudentId"]);
                        enrollment.Grade = read["Grade"].ToString();

                        enrollments.Add(enrollment);
                    }
                    if(read == null)
                    {

                        return HttpNotFound();

                    }

                }

                conn.Close();

            }

            return View(enrollments);
        }

        public ActionResult Details(int id)
        {

            Enrollment enrollment = new Enrollment();
            MySqlDataReader read;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using(MySqlCommand cmd = new MySqlCommand("_ViewEnrollment", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_Id", id);

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        enrollment.Id = Convert.ToInt32(read["Id"]);
                        enrollment.CourseId = Convert.ToInt32(read["CourseId"]);
                        enrollment.StudentId = Convert.ToInt32(read["StudentId"]);
                        enrollment.Grade = read["Grade"].ToString();

                    }
                    if(read == null)
                    {

                        return HttpNotFound();

                    }

                }

                conn.Close();
            }

            return View(enrollment);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Enrollment enrollment)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                        using(MySqlConnection conn = new MySqlConnection(ConnectionString))
                        {

                            conn.Open();

                            using(MySqlCommand cmd = new MySqlCommand("InsertEnrollmet", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@fkc", enrollment.CourseId);
                                cmd.Parameters.AddWithValue("@fkst", enrollment.StudentId);
                                cmd.Parameters.AddWithValue("@grade", enrollment.Grade);

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

                }//End IF

                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
            }
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {

            Enrollment enrollment = dbCtx.Enrollments.Find(id);

            if(enrollment == null)
            {

                return HttpNotFound();

            }

            return View(enrollment);
        }

        [HttpPost]
        public ActionResult Edit(Enrollment enrollment, int Id)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                        using(MySqlConnection conn = new MySqlConnection(ConnectionString))
                        {

                            conn.Open();

                            using(MySqlCommand cmd = new MySqlCommand("EditEnrollmetn", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@fkc", enrollment.CourseId);
                                cmd.Parameters.AddWithValue("@fkst", enrollment.StudentId);
                                cmd.Parameters.AddWithValue("@grade", enrollment.Grade);
                                cmd.Parameters.AddWithValue("@_id", Id);

                                cmd.ExecuteNonQuery();

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

                }//End if

                return RedirectToAction("Index");
            }//End try
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {

            Enrollment enrollment = dbCtx.Enrollments.Find(id);

            if(enrollment == null)
            {

                return HttpNotFound();

            }

            return View(enrollment);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {

                try
                {

                    MySqlDataReader read;
                    using(MySqlConnection conn = new MySqlConnection(ConnectionString))
                    {

                        conn.Open();

                        using(MySqlCommand cmd = new MySqlCommand("DeleteEnrollment", conn))
                        {

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@_id", id);

                            cmd.ExecuteNonQuery();

                        }

                    }

                }//End try
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

                        }//End secod foreach

                    }//End firts foreach
                    throw;

                }//End Catch

                return RedirectToAction("Index");
            }//End Try
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return View();
            }//End catch
        }//End Metod
        #endregion

    }//End Class
}//End Namespace
