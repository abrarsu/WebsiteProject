using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace JQueryAjaxInMVC2.Models
{
    public class InstructorViewModel
    {
        public int InstructorID { get; set; }

        [DisplayName("Club Name")]
        public int ClubID { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Email Address")]
        public string Email { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        public string Postcode { get; set; }

        [DisplayName("Club Name")]
        public string ClubName { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        //public string Salt { }
    }
}