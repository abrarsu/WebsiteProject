using JQueryAjaxInMVC2.Models;
using JQueryAjaxInMVC2.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            DBModel db = new DBModel();

            List<Instructor> instructorList = db.Instructors.ToList();

            InstructorViewModel instructorVM = new InstructorViewModel();

            List<InstructorViewModel> instructorVMList = instructorList.Select(x => new InstructorViewModel
            {
                InstructorID = x.InstructorID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ClubID = x.ClubID,
                ClubName = x.Club.ClubName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                AddressLine1 = x.AddressLine1,
                AddressLine2 = x.AddressLine2,
                Postcode = x.Postcode
                //Username = x.InstructorPassword.Username,
                //Password = x.InstructorPassword.Password
                
            }).ToList();

            return View(instructorVMList);
        }

  

        public ActionResult AddOrEditInstructor()
        {
            DBModel db = new DBModel();

            List<Club> list = db.Clubs.ToList();
            ViewBag.ClubList = new SelectList(list, "ClubID","ClubName");

            return View();
        }

        [HttpPost]
        public ActionResult SaveClub(InstructorViewModel instructorVM)
        {
            try
            {
                using (DBModel db = new DBModel())
                {
                    Instructor instructor = new Instructor();
                    instructor.FirstName = instructorVM.FirstName;
                    instructor.LastName = instructorVM.LastName;
                    instructor.ClubID = instructorVM.ClubID;
                    instructor.Email = instructorVM.Email;
                    instructor.PhoneNumber = instructorVM.PhoneNumber;
                    instructor.AddressLine1 = instructorVM.AddressLine1;
                    instructor.AddressLine2 = instructorVM.AddressLine2;
                    instructor.Postcode = instructorVM.Postcode;

                    if (instructorVM.InstructorID == 0)
                    {
                        db.Instructors.Add(instructorVM);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Entry(instructorVM).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", ViewAll()), message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }



     }
}