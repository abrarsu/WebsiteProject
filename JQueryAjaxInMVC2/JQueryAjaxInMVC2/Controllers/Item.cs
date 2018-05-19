using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JQueryAjaxInMVC2.Models;
namespace JQueryAjaxInMVC2.Controllers
{
    public class Item
    {
        private CustomerBooking booking = new CustomerBooking();

        public CustomerBooking Booking
        {
            get
            {
                return booking;
            }

            set
            {
                booking = value;
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

        public Item(CustomerBooking booking, int quantity)
        {
            this.Booking = booking;
            this.Quantity = quantity;
        }

      
    }
}