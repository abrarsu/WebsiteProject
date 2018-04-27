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

            if(id != 0)
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

                if(model.CustomerID > 0)
                {
                    //update

                }

            }
            catch
            {

            }
            return View();
        }
    }
}