using ContosoUniversity.Models;
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

        #region Index and Details
        public ActionResult Index()
        {

            List<Enrollment> enrollments = new List<Enrollment>();
            enrollments = dbCtx.Enrollments.OrderBy(x => x.Grade).ToList();

            return View(enrollments);
        }

        public ActionResult Details(int id)
        {

            Enrollment enrollment = dbCtx.Enrollments.Find(id);

            if(enrollment == null)
            {

                return HttpNotFound();

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

                        dbCtx.Enrollments.Add(enrollment);
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
            }
            catch
            {
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
        public ActionResult Edit(Enrollment enrollment)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                        dbCtx.Entry(enrollment).State = System.Data.Entity.EntityState.Modified;
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
            }//End try
            catch
            {
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

                    Enrollment enrollment = dbCtx.Enrollments.Find(id);

                    dbCtx.Enrollments.Remove(enrollment);
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
            }//End Try
            catch
            {
                return View();
            }//End catch
        }//End Metod
        #endregion

    }//End Class
}//End Namespace
