using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JQueryAjaxInMVC2.Models;

namespace JQueryAjaxInMVC2.Controllers
{
    public class ShoppingCartController : Controller
    {
        DBModel db = new DBModel();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookNow(int id)
        {
            if(Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.CustomerBookings.Find(id), 1));
                Session["cart"] = cart;
            }
            else
            {

            }
            return View("Cart");
        }
    }
}