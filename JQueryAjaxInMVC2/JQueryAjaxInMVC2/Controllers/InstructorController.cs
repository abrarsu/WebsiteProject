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

        
        public ActionResult AddOrEditInstructor(int id = 0)
        {
            BookingDBModel db = new BookingDBModel();

            IEnumerable<Club> clublist = db.Clubs.ToList();
            ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

            InstructorViewModel model = new InstructorViewModel();

            if (id != 0)
            {
                Instructor instructor = db.Instructors.SingleOrDefault(x => x.InstructorID == id);
                model.InstructorID = instructor.InstructorID;
                model.FirstName = instructor.FirstName;
                model.LastName = instructor.LastName;
                model.ClubID = instructor.ClubID;
                model.PhoneNumber = instructor.PhoneNumber;
                model.Email = instructor.Email;
                model.AddressLine1 = instructor.AddressLine1;
                model.AddressLine2 = instructor.AddressLine2;
                model.Postcode = instructor.Postcode;

                InstructorPassword instrPswd = db.InstructorPasswords.SingleOrDefault(x => x.InstructorID == id);
                model.Username = instrPswd.Username;
                model.Password = instrPswd.Password;

            }
        
            return View(model);
            
        }



        [HttpPost]
        public ActionResult SaveInstructor(InstructorViewModel model)
        {
            try
            {
                BookingDBModel db = new BookingDBModel();

                IEnumerable<Club> clublist = db.Clubs.ToList();
                ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

                if(model.InstructorID > 0)
                {
                    //Update
                    Instructor instruct = db.Instructors.SingleOrDefault(x => x.InstructorID == model.InstructorID);
                    instruct.FirstName = model.FirstName;
                    instruct.LastName = model.LastName;
                    instruct.ClubID = model.ClubID;
                    instruct.Email = model.Email;
                    instruct.PhoneNumber = model.PhoneNumber;
                    instruct.AddressLine1 = model.AddressLine1;
                    instruct.AddressLine2 = model.AddressLine2;
                    instruct.Postcode = model.Postcode;

                    db.SaveChanges();
                    int latestInstructorID = instruct.InstructorID;

                    var crypto = new SimpleCrypto.PBKDF2();

                    var encrpPass = crypto.Compute(model.Password);

                    InstructorPassword instrPswd = db.InstructorPasswords.SingleOrDefault(x => x.InstructorID == model.InstructorID);
                    instrPswd.Username = model.Username;
                    instrPswd.Password = encrpPass;
                    instrPswd.Salt = crypto.Salt;
                    instrPswd.InstructorID = latestInstructorID;

                    db.SaveChanges();

                    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAllInstructors", GetAllInstructors()), message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);


                }
                else {
                    //Insert
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

                    var crypto = new SimpleCrypto.PBKDF2();

                    var encrpPass = crypto.Compute(model.Password);
                    

                    instructorPswd.Username = model.Username;
                    instructorPswd.Password = encrpPass;
                    instructorPswd.Salt = crypto.Salt;
                    instructorPswd.InstructorID = latestInstructorID;

                    db.InstructorPasswords.Add(instructorPswd);
                    db.SaveChanges();

                    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAllInstructors", GetAllInstructors()), message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);


                }


            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);

            }
          
        }


        private bool IsValid(string username, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();

            bool isValid = false;

            using (var db = new BookingDBModel())
            {

            }



                return isValid;
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