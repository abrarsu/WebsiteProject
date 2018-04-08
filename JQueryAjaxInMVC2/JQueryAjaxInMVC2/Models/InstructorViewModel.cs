using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JQueryAjaxInMVC2.Models
{
    public class InstructorViewModel
    {
        public int InstructorID { get; set; }
        public int ClubID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Postcode { get; set; }

        public string ClubName { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        //public string Salt { }
    }
}