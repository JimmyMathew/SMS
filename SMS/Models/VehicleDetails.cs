using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public partial class VehicleDetails
    {
        public long vehicleId { get; set; }
        public long partyId { get; set; }
        public string partyName { get; set; }
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