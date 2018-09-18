using ContosoUniversity.Models;
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

            students = dbCtx.Students.OrderBy(x => x.LastName).ToList();



            return View(students);
        }

        public ActionResult Details(int id)
        {

            Student student = dbCtx.Students.Find(id);

            if(student == null)
            {

                return HttpNotFound();

            }

            return View(student);
        }
        #endregion

        #region Create camp

        public ActionResult Create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Create(Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        dbCtx.Students.Add(student);
                        dbCtx.SaveChanges();


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
            catch
            {
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
        public ActionResult Edit(Student student)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                        dbCtx.Entry(student).State = System.Data.Entity.EntityState.Modified;
                        dbCtx.SaveChanges();

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
            catch
            {
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

                    Student student = dbCtx.Students.Find(id);

                    dbCtx.Students.Remove(student);
                    dbCtx.SaveChanges();

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
            catch
            {
                return View();
            }
        }
        #endregion

    }
}
