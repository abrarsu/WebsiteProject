﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JQueryAjaxInMVC2.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.InstructorViewModel instructor)
        {
            if (ModelState.IsValid)
            {
                if(IsValid(instructor.Username, instructor.Password))
                {
                    FormsAuthentication.SetAuthCookie(instructor.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login Data is incorrect.");
                }

            }
            return View(instructor);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            //return RedirectToAction("","");
            return View();
        }



        private bool IsValid(string username, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();

            bool isValid = false;

            using (var db = new Models.BookingDBModel())
            {
                var instructor = db.InstructorPasswords.FirstOrDefault(u => u.Username == username);

                if(instructor != null)
                {
                    if(instructor.Password == crypto.Compute(password, instructor.Salt))
                    {
                        isValid = true;
                    }
                }
            }
                return isValid;

        } 

    }
}