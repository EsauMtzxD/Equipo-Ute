using ContosoUniversity.Models;
using System;
using System.Collections.Generic;
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

            instructors = dbCtx.Instructors.OrderBy(x => x.Id).ToList();

            return View(instructors);
        }

        public ActionResult Details(int id)
        {

            Instructor instructor = dbCtx.Instructors.Find(id);

            if(instructor == null)
            {

                return HttpNotFound();

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

                        dbCtx.Instructors.Add(instructor);
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

                }//End IF

                return RedirectToAction("Index");
            }//End try
            catch
            {
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
        public ActionResult Edit(Instructor instructor)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                        dbCtx.Entry(instructor).State = System.Data.Entity.EntityState.Modified;
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

                }//End if

                return RedirectToAction("Index");
            }//end try
            catch
            {
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

                    Instructor instructor = dbCtx.Instructors.Find(id);

                    dbCtx.Instructors.Remove(instructor);
                    dbCtx.SaveChanges();

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
            catch
            {
                return View();
            }//End Catch
        }//End Method
        #endregion

    }
}
