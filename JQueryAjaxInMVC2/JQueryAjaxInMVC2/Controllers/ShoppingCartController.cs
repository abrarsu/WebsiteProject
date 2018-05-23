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
        private DBModel db = new DBModel();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddtoCart(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.Classes.Find(id), 1));
                Session["cart"] = cart;
            }
            else
            {

            }
            return View("Cart");
        }

        //public ActionResult AddToCart(Class bookClass)
        //{
        //    if(Session["cart"] == null)
        //    {
        //        List<Class> list = new List<Class>();

        //        list.Add(bookClass);
        //        Session["cart"] = list;
        //        ViewBag.cart = list.Count();

        //        Session["count"] = 1;

        //    }
        //    else
        //    {
        //        List<Class> list = (List<Class>)Session["cart"];
        //        list.Add(bookClass);
        //        Session["cart"] = list;
        //        ViewBag.cart = list.Count();
        //        Session["count"] = Convert.ToInt32(Session["count"]) + 1;
        //    }
        //    return View();
        //}

        //public ActionResult MyBooking()
        //{
        //    return View((List<Class>)Session["cart"]);
        //}

        //public ActionResult Remove(Class booKClass)
        //{
        //    List<Class> li = (List<Class>)Session["cart"];
        //    li.RemoveAll(x => x.ClassID == booKClass.ClassID);
        //    Session["cart"] = li;
        //    Session["count"] = Convert.ToInt32(Session["count"]) - 1;

        //    return RedirectToAction("MyBooking", "AddToCart");
        //    //return View();
        //}
    }
}