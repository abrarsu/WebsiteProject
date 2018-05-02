using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JQueryAjaxInMVC2.Models
{
    public class CustomerViewModel
    {
        public int CustomerID { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage ="Please Enter First name", AllowEmptyStrings =false)]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please Enter Last name", AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [DisplayName("Belt level")]
        [Required(ErrorMessage = "Please Enter Select belt level", AllowEmptyStrings = false)]
        public int BeltLevel { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Please Enter a valid Email", AllowEmptyStrings = false)]
        public string Email { get; set; }

        [DisplayName("Phone number")]
        [Required(ErrorMessage = "Please Enter Phone number", AllowEmptyStrings = false)]
        public string PhoneNumber { get; set; }

        [DisplayName("Address Line 1")]
        [Required(ErrorMessage = "Please Enter Address Line 1", AllowEmptyStrings = false)]
        public string AddressLine1 { get; set; }

        [DisplayName("Address Line 2")]
        [Required(ErrorMessage = "Please Enter Address Line 2", AllowEmptyStrings = false)]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please Enter Postcode", AllowEmptyStrings = false)]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Please Enter Username", AllowEmptyStrings = false)]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is rrequired")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        [MaxLength(10, ErrorMessage = "Maximum 10 characters required")]
        public string Password { get; set; }

        public string BeltLevelColour { get; set; }
    }
}