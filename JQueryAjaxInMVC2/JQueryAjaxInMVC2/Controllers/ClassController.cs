﻿using JQueryAjaxInMVC2.Models;
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
            BookingDBModel db = new BookingDBModel();
            Class classes = new Class();
            ClassViewModel model = new ClassViewModel();

            IEnumerable<Club> clublist = db.Clubs.ToList();
            ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

            
            //this will get the firstName + lastName in the dropDown list
            var instructors = db.Instructors.Select(s => new { Text = s.FirstName + " " + s.LastName, Value = s.InstructorID }).ToList();
            ViewBag.InstructorList = new SelectList(instructors, "Value", "Text");

            if (id != 0)
            {
               classes = db.Classes.SingleOrDefault(x => x.ClassID == id);
                model.ClassID = classes.ClassID;
                model.ClubID = classes.ClubID;
                model.InstructorID = classes.InstructorID;
                model.ClassDate = classes.ClassDate;
                model.ClassTime = classes.ClassTime;
                model.ClassGIAGPrice = classes.ClassGIAGPrice;
                model.VenueName = classes.VenueName;
                model.AddressLine1 = classes.AddressLine1;
                model.AddressLine2 = classes.AddressLine2;
                model.Postcode = classes.Postcode;

            }

            return View(model);
        }


        [HttpPost]
        public ActionResult SaveClass(ClassViewModel model)
        {
            try
            {
                BookingDBModel db = new BookingDBModel();

                IEnumerable<Club> clublist = db.Clubs.ToList();
                ViewBag.ClubList = new SelectList(clublist, "ClubID", "ClubName");

                //this will get the firstName + lastName in the dropDown list
                var instructors = db.Instructors.Select(s => new { Text = s.FirstName + " " + s.LastName, Value = s.InstructorID }).ToList();
                ViewBag.InstructorList = new SelectList(instructors, "Value", "Text");

                //this checks 'if' if something has been added or not if not then it saves changes
                if (model.ClassID > 0)
                {
                    //Update
                   Class clas = db.Classes.SingleOrDefault(x => x.ClassID == model.ClassID);
                   
                    clas.ClubID = model.ClubID;
                    clas.InstructorID = model.InstructorID;
                    clas.ClassDate = model.ClassDate;
                    clas.ClassTime = model.ClassTime;
                    clas.ClassGIAGPrice = model.ClassGIAGPrice;
                    clas.VenueName = model.VenueName;
                    clas.AddressLine1 = model.AddressLine1;
                    clas.AddressLine2 = model.AddressLine2;
                    clas.Postcode = model.Postcode;

                    db.SaveChanges();

                    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllClasses()), message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    //This is going to insert data into the databse
                    Class classs = new Class();
                    model.ClassID = classs.ClassID;
                    model.ClubID = classs.ClubID;
                    model.InstructorID = classs.InstructorID;
                    model.ClassDate = classs.ClassDate;
                    model.ClassTime = classs.ClassTime;
                    model.ClassGIAGPrice = classs.ClassGIAGPrice;
                    model.VenueName = classs.VenueName;
                    model.AddressLine1 = classs.AddressLine1;
                    model.AddressLine2 = classs.AddressLine2;
                    model.Postcode = classs.Postcode;

                    db.Classes.Add(classs);
                    db.SaveChanges();

                    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllClasses()), message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);

                }

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