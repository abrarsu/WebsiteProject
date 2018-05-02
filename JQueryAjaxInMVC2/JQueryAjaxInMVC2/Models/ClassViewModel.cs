using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JQueryAjaxInMVC2.Models
{
    public class ClassViewModel
    {
        public int ClassID { get; set; }

        public int ClubID { get; set; }

        //[DisplayName("Instructor Name")]
        public int InstructorID { get; set; }

        [DisplayName("Class Date")]
        
        public System.DateTime ClassDate { get; set; }

        [DisplayName("Class Time")]
        public string ClassTime { get; set; }

        [DisplayName("GIAG Price")]
        public Nullable<decimal> ClassGIAGPrice { get; set; }

        [DisplayName("Venue Name")]
        public string VenueName { get; set; }

        [DisplayName(" Address Line 1")]
        public string AddressLine1 { get; set; }

        [DisplayName(" Address Line 2")]
        public string AddressLine2 { get; set; }
        public string Postcode { get; set; }


        [DisplayName("Class Name")]
        public string ClubName { get; set; }

        public string ClubDescription { get; set; }

        public Nullable<decimal> ClubMembership { get; set; }


        [DisplayName("Instructor Name")]
        public string FirstName { get; set; }

        [DisplayName("Instructor Name")]
        public string LastName { get; set; }
    }
}