using ContosoUniversity.Models;
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

        #region Index and Details
        public ActionResult Index()
        {

            List<OfficeAssignment> offices = new List<OfficeAssignment>();

            offices = dbCtx.OfficeAssignments.OrderBy(x => x.Id).ToList();


            return View(offices);
        }

        public ActionResult Details(int id)
        {

            OfficeAssignment office = dbCtx.OfficeAssignments.Find(id);

            if (office == null)
            {

                return HttpNotFound();

            }

            return View(office);
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

                        dbCtx.OfficeAssignments.Add(office);
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
        public ActionResult Edit(OfficeAssignment office)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    try
                    {

                        dbCtx.Entry(office).State = System.Data.Entity.EntityState.Modified;
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
            }//End Try
            catch
            {
                return View();
            }
        }//End Method
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {

            OfficeAssignment office = dbCtx.OfficeAssignments.Find(id);

            if (office == null)
            {

                return HttpNotFound();

            }

            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {

                try
                {

                    OfficeAssignment office = dbCtx.OfficeAssignments.Find(id);

                    dbCtx.OfficeAssignments.Remove(office);
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
            }
        }//En Method
        #endregion

    }
}
