using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JQueryAjaxInMVC2.Models;
namespace JQueryAjaxInMVC2.Controllers
{
    public class Item
    {
        private Class bookclass = new Class();

        public Class BookClass
        {
            get
            {
                return bookclass;
            }

            set
            {
                bookclass = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        private int quantity;

        public Item()
        {

        }

        public Item(Class bookclass, int quantity)
        {
            this.BookClass = bookclass;
            this.Quantity = quantity;
        }

      
    }
}