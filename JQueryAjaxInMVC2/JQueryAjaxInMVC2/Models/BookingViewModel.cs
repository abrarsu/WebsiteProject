using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JQueryAjaxInMVC2.Models
{
    public class BookingViewModel
    {
        public int BookingID { get; set; }
        public int ClassID { get; set; }
        public int CustomerID { get; set; }
        public Nullable<decimal> BookingTotalCost { get; set; }

        public int ClubID { get; set; }

        [DisplayName("Class Name")]
        public string ClubName { get; set; }

        [DisplayName("Customer Name")]
        public string FirstName { get; set; }

        [DisplayName("Customer Name")]
        public string LastName { get; set; }
    }
}