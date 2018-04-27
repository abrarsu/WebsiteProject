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

        public ActionResult RegisterOrEdit()
        {

            return View();
        }

        public ActionResult RegisterCustomer()
        {
            return View();
        }
    }
}