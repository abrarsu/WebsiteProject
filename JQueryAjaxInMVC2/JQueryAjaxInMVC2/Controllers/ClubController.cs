using JQueryAjaxInMVC2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            using (BookingDBModel db = new BookingDBModel())
            {
                return db.Clubs.ToList<Club>();
            }

        }

        public ActionResult AddOrEditClub(int id = 0)
        {
            Club club = new Club();
            if (id != 0)
            {
                using (BookingDBModel db = new BookingDBModel())
                {
                    club = db.Clubs.Where(x => x.ClubID == id).FirstOrDefault<Club>();
                }
            }
            return View(club);
        }

        [HttpPost]
        public ActionResult SaveClub(Club club)
        {
            try
            {
                using (BookingDBModel db = new BookingDBModel())
                {
                    //this checks 'if' if something has been added or not if not then it saves changes
                    if (club.ClubID == 0)
                    {
                        db.Clubs.Add(club);
                        db.SaveChanges();

                        return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllClubs()), message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        db.Entry(club).State = EntityState.Modified;
                        db.SaveChanges();

                        return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllClubs()), message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);

                    }
                }

            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                using (BookingDBModel db = new BookingDBModel())
                {
                    Club club = db.Clubs.Where(x => x.ClubID == id).FirstOrDefault<Club>();
                    db.Clubs.Remove(club);
                    db.SaveChanges();
                }
                //this will return the list of clubs to  be displayed
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllClubs()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}