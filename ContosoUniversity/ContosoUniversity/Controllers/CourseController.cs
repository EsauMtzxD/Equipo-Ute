using ContosoUniversity.Models;
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

            courses = dbCtx.Courses.OrderBy(x => x.Tittle).ToList();

            return View(courses);
        }

        public ActionResult Details(int id)
        {

            Course courses = dbCtx.Courses.Find(id);

            if(courses == null)
            {

                return HttpNotFound();

            }

            return View(courses);
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

                        dbCtx.Courses.Add(course);
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

                        dbCtx.Entry(course).State = System.Data.Entity.EntityState.Modified;
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

                }//End If

                return RedirectToAction("Index");
            }//End Try
            catch
            {
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

                    Course course = dbCtx.Courses.Find(id);

                    dbCtx.Courses.Remove(course);
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
        }//End Method
        #endregion

    }
}
