//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vehicle
    {
        public long vehicleId { get; set; }
        public long partyId { get; set; }
        public string vehicleType { get; set; }
        public string vehicleNo { get; set; }
        public Nullable<double> tareWeight { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string updatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    
        public virtual party_details party_details { get; set; }
    }
}
