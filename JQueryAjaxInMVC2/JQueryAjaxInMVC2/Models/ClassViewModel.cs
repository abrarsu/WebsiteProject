using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JQueryAjaxInMVC2.Models
{
    public class ClassViewModel
    {
        public int ClassID { get; set; }
        public int ClubID { get; set; }
        public int InstructorID { get; set; }
        public System.DateTime ClassDate { get; set; }
        public string ClassTime { get; set; }
        public Nullable<decimal> ClassGIAGPrice { get; set; }
        public string VenueName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Postcode { get; set; }


        public string ClubName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}