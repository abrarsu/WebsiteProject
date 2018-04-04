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
<<<<<<< HEAD
<<<<<<< HEAD
            DBModel db = new DBModel();

            List<Instructor> instructorList = db.Instructors.ToList();

            InstructorViewModel instructorVM = new InstructorViewModel();

            List<InstructorPassword> instructorPasswordlist = db.InstructorPasswords.ToList();
            ViewBag.instructorPasswordlist = new SelectList(instructorPasswordlist, "InstructorID", "Username");

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
                Username = x.InstructorPassword.Username,
                //Password = x.InstructorPassword.Password
                
            }).ToList();

            return View(instructorVMList);
=======
            return View(GetAllInstructors());
>>>>>>> parent of 216a606... inctructor view
=======
            return View(GetAllInstructors());
>>>>>>> parent of 216a606... inctructor view
        }

        IEnumerable<Instructor> GetAllInstructors()
        {
<<<<<<< HEAD
<<<<<<< HEAD
            DBModel db = new DBModel();

            List<Club> clublist = db.Clubs.ToList();
            ViewBag.ClubList = new SelectList(clublist, "ClubID","ClubName");

            //List<InstructorPassword> instructorPasswordlist = db.InstructorPasswords.ToList();
            //ViewBag.instructorPasswordlist = new SelectList(instructorPasswordlist, "InstructorID", "Username");

            return View();
        }

        //[HttpPost]
        //public ActionResult SaveInstructor(InstructorViewModel instructorVM)
        //{
        //    try
        //    {
        //        using (DBModel db = new DBModel())
        //        {
        //            List<Club> list = db.Clubs.ToList();
        //            ViewBag.ClubList = new SelectList(list, "ClubID", "ClubName");

        //            Instructor instructor = new Instructor();
        //            instructor.FirstName = instructorVM.FirstName;
        //            instructor.LastName = instructorVM.LastName;
        //            instructor.ClubID = instructorVM.ClubID;
        //            instructor.Email = instructorVM.Email;
        //            instructor.PhoneNumber = instructorVM.PhoneNumber;
        //            instructor.AddressLine1 = instructorVM.AddressLine1;
        //            instructor.AddressLine2 = instructorVM.AddressLine2;
        //            instructor.Postcode = instructorVM.Postcode;

        //            //this is for edit
        //            //if (instructorVM.InstructorID == 0)
        //            //{
        //                db.Instructors.Add(instructor);
        //                db.SaveChanges();

        //                int latestInstructorID = instructor.InstructorID;

        //                InstructorPassword instructorPassword = new InstructorPassword();
        //                instructorPassword.Username = instructorVM.Username;
        //                //instructorPassword.Password = instructorVM.Password;
        //                instructorPassword.InstructorID = latestInstructorID;

        //                db.InstructorPasswords.Add(instructorPassword);
        //                db.SaveChanges();

        //            //}
        //            //else
        //            //{
        //            //    db.Entry(instructorVM).State = EntityState.Modified;
        //            //    db.SaveChanges();
        //            //}

                   
        //        }

        //        return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", ViewAll()), message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {

        //        return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
        //    }

        //}



=======
            using (DBModel db = new DBModel())
            {
                return db.Instructors.ToList<Instructor>();
               
            }
        } 

=======
            using (DBModel db = new DBModel())
            {
                return db.Instructors.ToList<Instructor>();
               
            }
        } 

>>>>>>> parent of 216a606... inctructor view
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
<<<<<<< HEAD
>>>>>>> parent of 216a606... inctructor view
=======
>>>>>>> parent of 216a606... inctructor view
    }
}