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
    public class DepartamentController : Controller
    {

        private ContosoUniversityContext dbCtx = new ContosoUniversityContext();

        #region Index and Details
        public ActionResult Index()
        {

            List<Department> departments = new List<Department>();
            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            MySqlDataReader read;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using(MySqlCommand cmd = new MySqlCommand("_View", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        Department department = new Department();
                        department.Id = Convert.ToInt32(read["Id"]);
                        department.Name = read["Name"].ToString();
                        department.Budget = Convert.ToDouble(read["Budget"]);
                        department.StartDate = Convert.ToDateTime(read["StartDate"]);
                        department.InstructorId = Convert.ToInt32(read["InstructorId"]);

                        departments.Add(department);

                    }
                    if(read == null)
                    {

                        return HttpNotFound();

                    }

                }
                conn.Close();
            }

            return View(departments);
        }

        public ActionResult Details(int id)
        {

            Department d = new Department();
            string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
            MySqlDataReader read;
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {

                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("_ViewDepartament ", conn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@_Id", id);

                    read = cmd.ExecuteReader();

                    while (read.Read())
                    {

                        d.Name = read["Name"].ToString();
                        d.Budget = Convert.ToDouble(read["Budget"]);
                        d.StartDate = Convert.ToDateTime(read["StartDate"]);
                        d.InstructorId = Convert.ToInt32(read["InstructorId"]);

                    }
                    if(read == null)
                    {

                        return HttpNotFound();

                    }

                }
                conn.Close();
            }

            return View(d);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
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

                            using(MySqlCommand cmd = new MySqlCommand("InsertDepartment", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@_Name", department.Name);
                                cmd.Parameters.AddWithValue("@_Budget", department.Budget);
                                cmd.Parameters.AddWithValue("@_StartDate", department.StartDate);
                                cmd.Parameters.AddWithValue("@fk", department.InstructorId);

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
            catch(Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }//End Method
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {

            Department department = dbCtx.Departments.Find(id);

            if(department == null)
            {

                return HttpNotFound();

            }

            return View(department);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
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

                            using(MySqlCommand cmd = new MySqlCommand("EditDepartament", conn))
                            {

                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@_Name", department.Name);
                                cmd.Parameters.AddWithValue("@_Budget", department.Budget);
                                cmd.Parameters.AddWithValue("@_StartDate", department.StartDate);
                                cmd.Parameters.AddWithValue("@fk", department.InstructorId);
                                cmd.Parameters.AddWithValue("@_Id", department.Id);

                                read = cmd.ExecuteReader();

                                while (read.Read())
                                {

                                    department.Name = read["Name"].ToString();
                                    department.Budget = Convert.ToDouble(read["Budget"]);
                                    department.StartDate = Convert.ToDateTime(read["StartDate"]);
                                    department.InstructorId = Convert.ToInt32(read["InstructorId"]);

                                }
                                if(read == null)
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
            catch
            {
                return View();
            }
        }//End Method
        #endregion

        #region Delete
        public ActionResult Delete(int? id)
        {

            Department department = dbCtx.Departments.Find(id);

            if(department == null)
            {

                return HttpNotFound();

            }

            return View(department);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                try
                {

                    Department d = new Department();
                    string ConnectionString = "Server = localhost; Port = 3306; Database = contosouniversity; Uid = root; Pwd = 1234";
                    MySqlDataReader read;
                    using (MySqlConnection conn = new MySqlConnection(ConnectionString))
                    {

                        conn.Open();

                        using (MySqlCommand cmd = new MySqlCommand("DeleteDepartament ", conn))
                        {

                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@_Id", id);

                            read = cmd.ExecuteReader();

                            while (read.Read())
                            {

                                d.Name = read["Name"].ToString();
                                d.Budget = Convert.ToDouble(read["Budget"]);
                                d.StartDate = Convert.ToDateTime(read["StartDate"]);
                                d.InstructorId = Convert.ToInt32(read["InstructorId"]);

                            }
                            if (read == null)
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
            }
        }//End Method
        #endregion

    }
}
