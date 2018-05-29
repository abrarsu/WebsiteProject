using JQueryAjaxInMVC2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JQueryAjaxInMVC2.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View(GetAllClasses());
        }

        public IEnumerable<ClassViewModel> GetAllClasses()
        {
            using (DBModel db = new DBModel())
            {
                IEnumerable<Class> classList = db.Classes.ToList();
                //ClassViewModel classVM = new ClassViewModel();

                var classList2 = from m in db.Classes select m;

                //IEnumerable<ClassViewModel> classVMList;

                //foreach (var l in classList2)
                //{
                //    ClassViewModel newModel = new ClassViewModel();
                //    newModel.ClubID = l.ClubID;
                //    newModel.ClubName = l.
                //    classVMList.ToList().Add(l);
                //}

                IEnumerable<ClassViewModel> classVMList = classList.Select(x => new ClassViewModel
                {
                    ClassID = x.ClassID,
                    ClubID = x.ClubID,
                    //ClubName = x.Club.ClubName,
                    //ClubDescription = x.Club.ClubDescription,
                    //ClubMembership = x.Club.ClubMembership,
                    ClubName = db.Clubs.SingleOrDefault(y => y.ClubID == x.ClubID).ClubName,
                    ClubDescription = db.Clubs.SingleOrDefault(y => y.ClubID == x.ClubID).ClubDescription,
                    ClubMembership = db.Clubs.SingleOrDefault(y => y.ClubID == x.ClubID).ClubMembership,
                    FirstName = x.Instructor.FirstName,
                    LastName = x.Instructor.LastName,
                    ClassDate = x.ClassDate,
                    ClassTime = x.ClassTime,
                    ClassGIAGPrice = x.ClassGIAGPrice,
                    VenueName = x.VenueName,
                    AddressLine1 = x.AddressLine1,
                    AddressLine2 = x.AddressLine2,
                    Postcode = x.Postcode
                }).ToList();


                // return db.Classes.ToList<Class>();
                return classVMList;
            }

        }


        //use to display club detials into a patial view not working at the moment
        public ActionResult ClubInfo(int id)
        {
            DBModel db = new DBModel();
            Club currentClub = new Club();

            currentClub = db.Clubs.Find(id);

            return PartialView("_ClubInfo", currentClub);
        }

        //this is to copy the selected classto a differet table to be saved to the da
        public ActionResult SelectedClass(int id = 0)
        {
            DBModel db = new DBModel();
            Class selectedClass = new Class();
            ClassViewModel model = new ClassViewModel();
            if (id != 0)
            {   
               selectedClass = db.Classes.SingleOrDefault(x => x.ClassID == id);

            }
            return View(selectedClass);
        }


        [HttpPost]
        public ActionResult AddBooking(BookingViewModel model, int id)
        {
            DBModel db = new DBModel();
            int t = db.CustomerBookings.Count();
            Debug.Write("DB info: " + t);
            CustomerBooking booking = new CustomerBooking();
            //booking.ClassID = db.Classes.Where(x => x.ClassID == id).FirstOrDefault().ClassID;
            var s = db.Classes.Where(x => x.ClassID == id);
            foreach(var r in s)
            {
                booking.ClassID = r.ClassID;
                booking.BookingTotalCost = r.ClassGIAGPrice;
            }
            booking.CustomerID = db.CustomerPasswords.Where(x => x.Username == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault().CustomerID;
            //booking.BookingTotalCost = db.Classes.Where(x => x.ClassGIAGPrice == id).FirstOrDefault().ClassGIAGPrice;

            db.CustomerBookings.Add(booking);
            db.SaveChanges();

            //return View(model);
            return RedirectToAction("AddBooking", "Customer");
        }


        //returns/displays the registeration form
        public ActionResult RegisterOrEdit(int id = 0)
        {
            DBModel db = new DBModel();
            CustomerViewModel model = new CustomerViewModel();

            IEnumerable<GradeBelt> beltlist = db.GradeBelts.ToList();
            ViewBag.BeltList = new SelectList(beltlist, "BeltLevel", "BeltLevelColour");


            if (id != 0)
            {
                Customer cutomer = db.Customers.SingleOrDefault(x => x.CustomerID == id);
                model.CustomerID = cutomer.CustomerID;
                model.FirstName = cutomer.FirstName;
                model.LastName = cutomer.LastName;
                model.BeltLevel = cutomer.BeltLevel;
                model.Email = cutomer.Email;
                model.PhoneNumber = cutomer.PhoneNumber;
                model.AddressLine1 = cutomer.AddressLine1;
                model.AddressLine2 = cutomer.AddressLine2;
                model.Postcode = cutomer.Postcode;

                CustomerPassword customerPswd = db.CustomerPasswords.SingleOrDefault(x => x.CustomerID == id);
                model.Username = customerPswd.Username;
                model.Password = customerPswd.Password;
            }


            return View(model);
        }

        //Checks if customer exists first if not saves to database
        [HttpPost]
        public ActionResult SaveCustomer(CustomerViewModel model)
        {
            
            try
            {
                DBModel db = new DBModel();

                IEnumerable<GradeBelt> beltlist = db.GradeBelts.ToList();
                ViewBag.BeltList = new SelectList(beltlist, "BeltLevel", "BeltLevelColour");

                if (model.CustomerID > 0)
                {
                    //this part update data of existing customers
                    Customer customer = db.Customers.SingleOrDefault(x => x.CustomerID == model.CustomerID);
                    customer.FirstName = model.FirstName;
                    customer.LastName = model.LastName;
                    customer.BeltLevel = model.BeltLevel;
                    customer.Email = model.Email;
                    customer.PhoneNumber = model.PhoneNumber;
                    customer.AddressLine1 = model.AddressLine1;
                    customer.AddressLine2 = model.AddressLine2;
                    customer.Postcode = model.Postcode;

                    db.SaveChanges();
                    int latestCustomerID = customer.CustomerID;

                    var crypto = new SimpleCrypto.PBKDF2();

                    var encrpPass = crypto.Compute(model.Password);

                    CustomerPassword customerPswd = db.CustomerPasswords.SingleOrDefault(x => x.CustomerID == model.CustomerID);
                    customerPswd.Username = model.Username;
                    customerPswd.Password = encrpPass;
                    customerPswd.Salt = crypto.Salt;
                    customerPswd.CustomerID = latestCustomerID;

                    db.SaveChanges();

                    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Index"), message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    
                   //insert
                    Customer custom = new Customer();
                    custom.FirstName = model.FirstName;
                    custom.LastName = model.LastName;
                    custom.BeltLevel = model.BeltLevel;
                    custom.Email = model.Email;
                    custom.PhoneNumber = model.PhoneNumber;
                    custom.AddressLine1 = model.AddressLine1;
                    custom.AddressLine2 = model.AddressLine2;
                    custom.Postcode = model.Postcode;

                    db.Customers.Add(custom);
                    db.SaveChanges();


                    int latestCustomerID = custom.CustomerID;

                    CustomerPassword customPswd = new CustomerPassword();

                    var crypto = new SimpleCrypto.PBKDF2();

                    var encrpPass = crypto.Compute(model.Password);


                    customPswd.Username = model.Username;
                    customPswd.Password = encrpPass;
                    customPswd.Salt = crypto.Salt;
                    customPswd.CustomerID = latestCustomerID;

                    db.CustomerPasswords.Add(customPswd);
                    db.SaveChanges();

                    ViewBag.Message = "Registered Successfully";

                    return RedirectToAction("Index", "Customer");



                    //return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Index", GetAllClasses()), message = "Registered Successfully" },JsonRequestBehavior.AllowGet);  
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);

            }
            //return View();
        }



        //Displays the login form
        [HttpGet]
        public ActionResult customerLogin()
        {
            return View();
        }

        //checks if login details are correct and redirects to the relavent page
        [HttpPost]
        public ActionResult CustomerLogin(CustomerPassword customer)
        {
            if(ModelState.IsValid)
            {
                if(IsValid(customer.Username, customer.Password))
                {
                    FormsAuthentication.SetAuthCookie(customer.Username, false);
                    return RedirectToAction("Index", "Customer");
                }
            }
            else
            {
                ModelState.AddModelError("", "Login Data is incorrect");
            }
            return View(customer);
        }


        //logs out user and takes them back to home page
        public ActionResult CustomerLogout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Customer");
        }

        //validates and matches the data the user enters with the data already existing 
        //in the database
        private bool IsValid(string username, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();

            bool isValid = false;

            using (var db = new DBModel())
            {
                var customer = db.CustomerPasswords.FirstOrDefault(c => c.Username == username);
                if (customer != null)
                {
                    if (customer.Password == crypto.Compute(password, customer.Salt))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
    }
}