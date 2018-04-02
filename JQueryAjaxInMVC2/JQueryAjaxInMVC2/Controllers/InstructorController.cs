using JQueryAjaxInMVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryAjaxInMVC2.Controllers
{
    public class InstructorController : Controller
    {
        // GET: Instructor
        public ActionResult Index()
        {
            return View();
        } 

        public ActionResult ViewAll()
        {
            return View(GetAllInstructors());
        }

        IEnumerable<Instructor> GetAllInstructors()
        {
            using (DBModel db = new DBModel())
            {
                return db.Instructors.ToList<Instructor>();
               
            }
        } 

        public ActionResult AddOrEditInstructor(int id =0)
        {
            Instructor instructor = new Instructor();
            if (id !=0)
            {
                using (DBModel db = new DBModel())
                {
                    instructor = db.Instructors.Where(x => x.ClubID == id).FirstOrDefault<Instructor>();
                    
                }

            }
            return View(instructor);
        }
    }
}