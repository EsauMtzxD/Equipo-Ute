using ContosoUniversity.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class InstructorController : Controller
    {

        private ContosoUniversityContext dbCtx = new ContosoUniversityContext();

        #region Index and Details
        public ActionResult Index()
        {

            List<Instructor> instructors = new List<Instructor>();

            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            MySqlDataReader read;
            
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using(MySqlCommand cmd = new MySqlCommand("ViewInstructor", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        Instructor instructor = new Instructor();
                        instructor.Id = Convert.ToInt32(read["Id"]);
                        instructor.LastName = read["LastName"].ToString();
                        instructor.FirstMidName = read["FirstMidName"].ToString();
                        instructor.HireDate = Convert.ToDateTime(read["HireDate"]);

                        instructors.Add(instructor);

                    }


                }

                conn.Close();
            }

            return View(instructors);
        }

        public ActionResult Details(int id)
        {

            Instructor instructor = new Instructor();
            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            MySqlDataReader read;
            using(MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using(MySqlCommand cmd = new MySqlCommand("_ViewInstructor", conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_Id", id);

                    read = cmd.ExecuteReader();


                    while (read.Read())
                    {

                        instructor.LastName = read["LastName"].ToString();
                        instructor.FirstMidName = read["FirstMidName"].ToString();
                        instructor.HireDate = Convert.ToDateTime(read["HireDate"]);

                    }
                    if (instructor == null)
                    {

                        return HttpNotFound();

                    }

                }

                conn.Close();

            }

            return View(instructor);
        }

        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Instructor instructor)
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

                            using(MySqlCommand cmd = new MySqlCommand("InsertInstructor", conn))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@Lastn", instructor.LastName);
                                cmd.Parameters.AddWithValue("@Firstn", instructor.FirstMidName);
                                cmd.Parameters.AddWithValue("@Hire", instructor.HireDate);

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
            }//End try
            catch(Exception e)
            {

                Console.WriteLine(e.Message);

                return View();
            }
         }//End Method
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {

            Instructor instructor = dbCtx.Instructors.Find(id);

            if(instructor == null)
            {

                return HttpNotFound();

            }

            return View(instructor);
        }

        [HttpPost]
        public ActionResult Edit(Instructor instructor, int id)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                        string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
                        MySqlDataReader read;
                        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                        {

                            conn.Open();

                            using(MySqlCommand cmd = new MySqlCommand("EditInstructor", conn))
                            {

                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@_Last", instructor.LastName);
                                cmd.Parameters.AddWithValue("@_First", instructor.FirstMidName);
                                cmd.Parameters.AddWithValue("@Hire", instructor.HireDate);
                                cmd.Parameters.AddWithValue("@_Id", id);

                                read = cmd.ExecuteReader();


                                while (read.Read())
                                {

                                    instructor.LastName = read["LastName"].ToString();
                                    instructor.FirstMidName = read["FirstMidName"].ToString();
                                    instructor.HireDate = Convert.ToDateTime(read["HireDate"]);

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

                }//End if

                return RedirectToAction("Index");
            }//end try
            catch(Exception e)
            {

                Console.WriteLine(e);

                return View();
            }
        }//End method
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {

            Instructor instructor = dbCtx.Instructors.Find(id);

            if(instructor == null)
            {

                return HttpNotFound();

            }

            return View(instructor);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {

                try
                {
                    string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";

                    Instructor instructor = new Instructor();
                    MySqlDataReader read;
                    using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                    {

                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand("DeleteInstructor", conn))
                        {

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@_Id", id);

                            read = cmd.ExecuteReader();


                            while (read.Read())
                            {

                                instructor.LastName = read["LastName"].ToString();
                                instructor.FirstMidName = read["FirstMidName"].ToString();
                                instructor.HireDate = Convert.ToDateTime(read["HireDate"]);

                            }
                            if(read == null)
                            {

                                return HttpNotFound();

                            }

                        }

                        conn.Close();

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
            }//End try
            catch(Exception e)
            {

                Console.WriteLine(e);

                return View();
            }//End Catch
        }//End Method
        #endregion

    }
}
