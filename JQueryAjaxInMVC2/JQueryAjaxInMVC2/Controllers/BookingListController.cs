using JQueryAjaxInMVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQueryAjaxInMVC2.Controllers
{
    public class BookingListController : Controller
    {
        // GET: BookingList
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewBookingList()
        {
            return View(GetAllBookings());
        }

        public IEnumerable<BookingViewModel> GetAllBookings()
        {
            using (DBModel db = new DBModel())
            {
                IEnumerable<CustomerBooking> customerBookings = db.CustomerBookings.ToList();

                IEnumerable<BookingViewModel> bookingList = customerBookings.Select(x => new BookingViewModel
                {
                    BookingID = x.BookingID,
                    ClassID = x.ClassID,
                    ClubID = x.Class.ClubID,
                    ClubName = x.Class.Club.ClubName,
                    ClassDate = x.Class.ClassDate,
                    ClassTime = x.Class.ClassTime,
                    InstructorID = x.Class.InstructorID,
                    instrFirstName = x.Class.Instructor.FirstName,
                    instrLastName = x.Class.Instructor.LastName,
                    CustomerID = x.CustomerID,
                    FirstName = x.Customer.FirstName,
                    LastName = x.Customer.LastName,
                    BeltLevel = x.Customer.GradeBelt.BeltLevel,
                    BeltLevelColour = x.Customer.GradeBelt.BeltLevelColour,
                    ClassGIAGPrice = x.Class.ClassGIAGPrice,
                    VenueName = x.Class.VenueName,
                    AddressLine1 = x.Class.AddressLine1,
                    AddressLine2 = x.Class.AddressLine2,
                    Postcode = x.Class.Postcode


                }).ToList();

                return bookingList;
            }
        }
    }
}