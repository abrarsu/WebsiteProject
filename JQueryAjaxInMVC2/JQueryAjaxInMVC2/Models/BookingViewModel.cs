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

        public System.DateTime ClassDate { get; set; }

        public string ClassTime { get; set; }


        [DisplayName("Customer Name")]
        public string FirstName { get; set; }

        [DisplayName("Customer Name")]
        public string LastName { get; set; }

        public int BeltLevel { get; set; }

        public string instrFirstName { get; set; }

        public string instrLastName { get; set; }

        public int InstructorID { get; set; }

        public Nullable<decimal> ClassGIAGPrice { get; set; }

        public string BeltLevelColour { get; set; }

        [DisplayName("Venue Name")]
        public string VenueName { get; set; }

        [DisplayName(" Address Line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName(" Address Line 2")]
        public string AddressLine2 { get; set; }
        public string Postcode { get; set; }



    }
}