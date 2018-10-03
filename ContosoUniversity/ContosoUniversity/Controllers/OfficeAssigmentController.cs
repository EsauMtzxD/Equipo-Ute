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
    public class OfficeAssigmentController : Controller
    {

        private ContosoUniversityContext dbCtx = new ContosoUniversityContext();
        string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";


        #region Index and Details
        public ActionResult Index()
        {

            List<OfficeAssignment> offices = new List<OfficeAssignment>();
            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            MySqlDataReader read;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("ViewOffice", conn))
                {  

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        OfficeAssignment o = new OfficeAssignment();
                        o.Id = Convert.ToInt32(read["Id"]);
                        o.Location = read["Location"].ToString();
                        o.InstructorId = Convert.ToInt32(read["InstructorId"]);

                        offices.Add(o);

                    }
                    if (read == null)
                    {

                        return HttpNotFound();

                    }

                }
                conn.Close();
            }

            return View(offices);
        }

        public ActionResult Details(int id)
        {

            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            OfficeAssignment o = new OfficeAssignment();
            MySqlDataReader read;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("_ViewOffice", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_Id", id);

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        o.Id = Convert.ToInt32(read["Id"]);
                        o.Location = read["Location"].ToString();
                        o.InstructorId = Convert.ToInt32(read["InstructorId"]);

                    }
                    if (read == null)
                    {

                        return HttpNotFound();

                    }
                }
                conn.Close();
            }

            return View(o);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OfficeAssignment office)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                        string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
                        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                        {

                            conn.Open();

                            using (MySqlCommand cmd = new MySqlCommand("InsertOfficeAssignment", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@Loc", office.Location);
                                cmd.Parameters.AddWithValue("@fk", office.InstructorId);

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
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
            }
        }//En Method
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {

            OfficeAssignment office = dbCtx.OfficeAssignments.Find(id);

            if (office == null)
            {

                return HttpNotFound();

            }


            return View();
        }

        [HttpPost]
        public ActionResult Edit(OfficeAssignment office, int id)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                        MySqlDataReader read;
                        using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                        {

                            conn.Open();

                            using (MySqlCommand cmd = new MySqlCommand("EditOffice", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@_Loc", office.Location);
                                cmd.Parameters.AddWithValue("@fk", office.InstructorId);
                                cmd.Parameters.AddWithValue("@_id", id);

                                read = cmd.ExecuteReader();

                                while (read.Read())
                                {

                                    office.Location = read["Location"].ToString();
                                    office.InstructorId = Convert.ToInt32(read["InstructorId"]);

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
            }//End Try
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
            }
        }//End Method
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {

            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            OfficeAssignment o = new OfficeAssignment();
            MySqlDataReader read;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("_ViewOffice", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_Id", id);

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        o.Location = read["Location"].ToString();
                        o.InstructorId = Convert.ToInt32(read["InstructorId"]);

                    }
                    if (read == null)
                    {

                        return HttpNotFound();

                    }
                }
                conn.Close();
            }

            return View(o);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection )
        {
            try
            {

                try
                {

                    OfficeAssignment office = new OfficeAssignment();
                    MySqlDataReader read;
                    using(MySqlConnection conn = new MySqlConnection(ConnectionString))
                    {

                        conn.Open();

                        using(MySqlCommand cmd = new MySqlCommand("DeleteOffice", conn))
                        {

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@_Id", id);

                            read = cmd.ExecuteReader();

                            while (read.Read())
                            {

                                office.Location = read["Location"].ToString();
                                office.InstructorId = Convert.ToInt32(read["InstrucorId"]);

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
            }//End Try
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
            }
        }//En Method
        #endregion

    }
}
