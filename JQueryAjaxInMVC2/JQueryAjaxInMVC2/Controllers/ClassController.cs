using JQueryAjaxInMVC2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryAjaxInMVC2.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ViewAll()
        {
            return View(GetAllClasses());
        }

        public IEnumerable<ClassViewModel> GetAllClasses()
        {
            using (BookingDBModel db = new BookingDBModel())
            {
                IEnumerable<Class> classList = db.Classes.ToList();
                //ClassViewModel classVM = new ClassViewModel();

                IEnumerable<ClassViewModel> classVMList = classList.Select(x => new ClassViewModel
                {
                    ClassID = x.ClassID,
                    ClubID = x.ClubID,
                    ClubName = x.Club.ClubName,
                    FirstName = x.Instructor.FirstName,
                    LastName = x.Instructor.LastName,
                    ClassDate = x.ClassDate,
                    ClassTime = x.ClassTime,
                    ClassGIAGPrice = x.ClassGIAGPrice,
                    VenueName = x.VenueName,
                    AddressLine1 = x.AddressLine1,
                    AddressLine2 = x.AddressLine2,
                    Postcode = x.Postcode}).ToList();

                
               // return db.Classes.ToList<Class>();
                return  classVMList;
            }

        }

       
        public ActionResult AddOrEditClass(int id = 0)
        {
            Class classes = new Class();
            //if(id != 0)
            //{
                using (BookingDBModel db = new BookingDBModel())
                {
                    IEnumerable<Club> clublist = db.Clubs.ToList();
                    ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

                    IEnumerable<Instructor> instructorlist = db.Instructors.ToList();
                    ViewBag.instructorlist = new SelectList(instructorlist, "InstructorID", "FirstName", "LastName");

                    classes = db.Classes.Where(x => x.ClassID == id).FirstOrDefault<Class>();
                }
            //}

            return View();
        }


        [HttpPost]
        public ActionResult SaveClass(Class classes)
        {
            try
            {
                using (BookingDBModel db = new BookingDBModel())
                {
                    //this checks 'if' if something has been added or not if not then it saves changes
                    if (classes.ClassID == 0)
                    {
                        //IEnumerable<Club> clublist = db.Clubs.ToList();
                        //ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

                        //IEnumerable<Instructor> instructorlist = db.Instructors.ToList();
                        //ViewBag.instructorlist = new SelectList(instructorlist, "InstructorID", "FirstName", "LastName");



                        db.Classes.Add(classes);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(classes).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllClasses()), message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
        }


        public ActionResult DeleteClass( int id)
        {
            try
            {
                using (BookingDBModel db = new BookingDBModel())
                {
                    Class classes = db.Classes.Where(x => x.ClassID == id).FirstOrDefault<Class>();
                    db.Classes.Remove(classes);
                    db.SaveChanges();
                }
                //this will return the list of classes to  be displayed
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllClasses()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}