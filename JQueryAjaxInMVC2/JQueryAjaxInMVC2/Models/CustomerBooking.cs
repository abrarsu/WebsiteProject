//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JQueryAjaxInMVC2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerBooking
    {
        public int BookingID { get; set; }
        public int ClassID { get; set; }
        public int CustomerID { get; set; }
        public Nullable<decimal> BookingTotalCost { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
