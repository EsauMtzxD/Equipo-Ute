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
    public class CourseController : Controller
    {

        private ContosoUniversityContext dbCtx = new ContosoUniversityContext();

        #region Index and Details
        public ActionResult Index()
        {

            List<Course> courses = new List<Course>();
            
            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            MySqlDataReader read;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using(MySqlCommand cmd = new MySqlCommand("ViewCourse", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        Course c = new Course();
                        c.Id = Convert.ToInt32(read["Id"]);
                        c.Tittle = read["Tittle"].ToString();
                        c.Credits = Convert.ToInt32(read["Credits"]);
                        c.DepartamentId = Convert.ToInt32(read["DepartamentId"]);

                        courses.Add(c);

                    }
                    if(read == null)
                    {

                        return HttpNotFound();

                    }

                }

                conn.Close();

            }

            return View(courses);
        }

        public ActionResult Details(int id)
        {

            Course c = new Course();
            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            MySqlDataReader read;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using(MySqlCommand cmd = new MySqlCommand("_ViewCourse", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_Id",id);

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        c.Tittle = read["Tittle"].ToString();
                        c.Credits = Convert.ToInt32(read["Credits"]);
                        c.DepartamentId = Convert.ToInt32(read["DepartamentId"]);

                    }
                    if(read == null)
                    {

                        return HttpNotFound();

                    }

                }

                conn.Close();

            }

            return View(c);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
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

                            using(MySqlCommand cmd = new MySqlCommand("InsertCourse", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@_Tittle", course.Tittle);
                                cmd.Parameters.AddWithValue("@_Credits", course.Credits);
                                cmd.Parameters.AddWithValue("@_Departament", course.DepartamentId);

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
            }//End Try
            catch
            {
                return View();
            }
        }//End Method
        #endregion

        #region Edit Camp
        public ActionResult Edit(int id)
        {

            Course course = dbCtx.Courses.Find(id);

            if(course == null)
            {

                return HttpNotFound();

            }

            return View(course);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
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

                            using(MySqlCommand cmd = new MySqlCommand("EditCourse", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@_Tittle", course.Tittle);
                                cmd.Parameters.AddWithValue("@_Credits", course.Credits);
                                cmd.Parameters.AddWithValue("@_Departament", course.DepartamentId);
                                cmd.Parameters.AddWithValue("@_Id", course.Id);

                                read = cmd.ExecuteReader();

                            }
                            while (read.Read())
                            {

                                course.Tittle = read["Tittle"].ToString();
                                course.Credits = Convert.ToInt32(read["Credits"]);
                                course.DepartamentId = Convert.ToInt32(read["DepartamentId"]);

                            }
                            if(read == null)
                            {

                                return HttpNotFound();

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

                }//End If

                return RedirectToAction("Index");
            }//End Try
            catch(Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }//Fin Method
        #endregion

        #region Delete Camp
        public ActionResult Delete(int? id)
        {

            Course course = dbCtx.Courses.Find(id);

            if(course == null)
            {

                return HttpNotFound();

            }

            return View(course);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                try
                {

                    Course c = new Course();
                    string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
                    MySqlDataReader read;
                    using(MySqlConnection conn = new MySqlConnection(ConnectionString))
                    {

                        conn.Open();

                        using(MySqlCommand cmd = new MySqlCommand("DeleteCourse", conn))
                        {

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@_Id", c.Id);

                            read = cmd.ExecuteReader();

                            while (read.Read())
                            {

                                c.Tittle = read["Tittle"].ToString();
                                c.Credits = Convert.ToInt32(read["Credits"]);
                                c.DepartamentId = Convert.ToInt32(read["DepartamentId"]);

                            }
                            if(read == null)
                            {

                                return HttpNotFound();

                            }

                            conn.Close();

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
            catch(Exception e)
            {
                Console.WriteLine(e);
                return View();
            }//End catch
        }//End Method
        #endregion

    }
}
