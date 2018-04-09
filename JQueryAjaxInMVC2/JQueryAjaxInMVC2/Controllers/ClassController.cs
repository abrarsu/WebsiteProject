using JQueryAjaxInMVC2.Models;
using System;
using System.Collections.Generic;
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
            if(id != 0)
            {
                using (BookingDBModel db = new BookingDBModel())
                {
                    IEnumerable<Club> clublist = db.Clubs.ToList();
                    ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

                    IEnumerable<Instructor> instructorlist = db.Instructors.ToList();
                    ViewBag.instructorlist = new SelectList(instructorlist, "InstructorID", "FirstName", "LastName");

                    classes = db.Classes.Where(x => x.ClassID == id).FirstOrDefault<Class>();
                }
            }

            return View(classes);
        }


        [HttpPost]
        public ActionResult SaveClass()
        {
            return View();
        }


        public ActionResult DeleteClass()
        {
            return View();
        }
    }
}