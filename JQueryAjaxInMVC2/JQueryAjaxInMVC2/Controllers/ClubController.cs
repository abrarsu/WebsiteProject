using JQueryAjaxInMVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryAjaxInMVC2.Controllers
{
    public class ClubController : Controller
    {
        // GET: Club
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllClubs());
        }

        IEnumerable<Club> GetAllClubs()
        {
            using (DBModel db = new DBModel())
            {
                return db.Clubs.ToList<Club>();
            }

        }

        public ActionResult AddOrEditClub(int id = 0)
        {
            Club club = new Club();
            return View(club);
        } 

        [HttpPost]
        public ActionResult SaveClub(Club club)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    db.Clubs.Add(club);
                    db.SaveChanges();
                }
                //return RedirectToAction("ViewAll");
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "VeiwAll", GetAllClubs()), message = "Submitted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}