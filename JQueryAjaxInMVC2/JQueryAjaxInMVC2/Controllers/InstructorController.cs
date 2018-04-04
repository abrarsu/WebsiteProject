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

        public ActionResult ViewAllInstructors()
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
                Postcode = x.Postcode,
                Username = x.InstructorPassword.Username 
            }).ToList();

            return View(instructorVMList);
        }

        public ActionResult AddOrEditInstructor()
        {
            DBModel db = new DBModel();

            List<Club> clublist = db.Clubs.ToList();
            ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

            List<InstructorPassword> instructorPasswordlist = db.InstructorPasswords.ToList();
            ViewBag.instructorPasswordlist = new SelectList(instructorPasswordlist, "InstructorID", "Username");

            return View();
        }

        [HttpPost]
        public ActionResult SaveInstructor(InstructorViewModel model)
        {
            try
            {
                using (DBModel db = new DBModel())
                { 

                    List<Club> clublist = db.Clubs.ToList();
                    ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

                    List<InstructorPassword> instructorPasswordlist = db.InstructorPasswords.ToList();
                    ViewBag.instructorPasswordlist = new SelectList(instructorPasswordlist, "InstructorID", "Username");

                    Instructor instructor = new Instructor();
                    instructor.FirstName = model.FirstName;
                    instructor.LastName = model.LastName;
                    instructor.ClubID = model.ClubID;
                    instructor.Email = model.Email;
                    instructor.PhoneNumber = model.PhoneNumber;
                    instructor.AddressLine1 = model.AddressLine1;
                    instructor.AddressLine2 = model.AddressLine2;
                    instructor.Postcode = model.Postcode;

                    db.Instructors.Add(instructor);
                    db.SaveChanges();


                    int latestInstructorID = instructor.InstructorID;

                    InstructorPassword instructorPswd = new InstructorPassword();
                    instructorPswd.Username = model.Username;
                    instructorPswd.Password = model.Password;
                    instructorPswd.InstructorID = latestInstructorID;

                    db.InstructorPasswords.Add(instructorPswd);
                    db.SaveChanges();
                }

                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAllInstructors", ViewAllInstructors()), message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                //throw ex;
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);

            }
            //return View(model);
        }

    

    }
}