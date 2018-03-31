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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderProductItems = new HashSet<OrderProductItem>();
        }
    
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int OrderStatusID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public decimal OrderTotalCost { get; set; }
        public string PaymentMethod { get; set; }
        public string DeliveryNote { get; set; }
        public string CustomerName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Postcode { get; set; }
    
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProductItem> OrderProductItems { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
    }
}
