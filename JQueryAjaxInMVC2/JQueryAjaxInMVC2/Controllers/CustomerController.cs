using JQueryAjaxInMVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace JQueryAjaxInMVC2.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

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
                    //update
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

                    return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "Index"), message = "Registered Successfully" }, JsonRequestBehavior.AllowGet);


                }
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);

            }
            //return View();
        }

        //private bool IsValid(string username, string password)
        //{
        //    var crypto = new SimpleCrypto.PBKDF2();

        //    bool isValid = false;

        //    using (var db = new DBModel())
        //    {

        //    }



        //    return isValid;
        //}
    }
}