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
            return View(GetAllInstructors());
        }


        public IEnumerable<InstructorViewModel> GetAllInstructors()
        {

            using (BookingDBModel db = new BookingDBModel())
            {
                IEnumerable<Instructor> instructorList = db.Instructors.ToList();

                //InstructorViewModel instructorVM = new InstructorViewModel();

                IEnumerable<InstructorViewModel> instructorVMList = instructorList.Select(x => new InstructorViewModel
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
                    Username = db.InstructorPasswords.SingleOrDefault(y => y.InstructorID == x.InstructorID).Username
                }).ToList();

                return instructorVMList;
            }

        }

        //this need some more work
        public ActionResult AddOrEditInstructor(int id = 0)
        {
            Instructor instructor = new Instructor();
            InstructorPassword instructorPswd = new InstructorPassword();
            InstructorViewModel instructorVM = new InstructorViewModel();

            //if(id != 0)
            //{
            using (BookingDBModel db = new BookingDBModel())
            {
                    IEnumerable<Club> clublist = db.Clubs.ToList();
                    ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");
                    //instructor = db.Instructors.Where(x => x.InstructorID == id).FirstOrDefault<Instructor>();
                    //instructorPswd = db.InstructorPasswords.Where(x => x.InstructorID == id).FirstOrDefault<InstructorPassword>();
                   
                    
                }

           // }
        
            //return View(instructor);
            return View();
        }



        [HttpPost]
        public ActionResult SaveInstructor(InstructorViewModel model)
        {
            try
            {
                BookingDBModel db = new BookingDBModel();


                //List<Club> clublist = db.Clubs.ToList();
                IEnumerable<Club> clublist = db.Clubs.ToList();
                ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

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
                //instructorPswd.InstructorID = model.InstructorID;
                instructorPswd.Username = model.Username;
                instructorPswd.Password = model.Password;
                instructorPswd.InstructorID = latestInstructorID;

                db.InstructorPasswords.Add(instructorPswd);
                db.SaveChanges();


                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAllInstructors", GetAllInstructors()), message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {
                //throw ex;
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);

            }
          // return View(model);
        }


        public ActionResult DeleteInstructor(int id)
        {
            try
            {
                BookingDBModel db = new BookingDBModel();

                
                    InstructorPassword instructorPswd = db.InstructorPasswords.Where(x => x.InstructorID == id).FirstOrDefault<InstructorPassword>();
                    db.InstructorPasswords.Remove(instructorPswd);
                    db.SaveChanges();

                    Instructor instructor = db.Instructors.Where(x => x.InstructorID == id).FirstOrDefault<Instructor>();
                    db.Instructors.Remove(instructor);
                    db.SaveChanges();

                
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAllInstructors", GetAllInstructors()), message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);

            }

            // return View();
        }
    }
 }