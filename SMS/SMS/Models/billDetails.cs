using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public class billDetails
    {

        public long billNo { get; set; }
        public string dateBill { get; set; }
        public string toName { get; set; }
        public string addLine1 { get; set; }
        public string addLine2 { get; set; }
        public string addLine3 { get; set; }
        public string transportMode { get; set; }
        public string vehicleNoBill { get; set; }
        public string placeOfDeliveryBill { get; set; }
        public string materialDescription { get; set; }
        public string netWeightQty { get; set; }
        public Nullable<double> rateBill { get; set; }
        public Nullable<double> amountBill { get; set; }
        public Nullable<double> totalAmountBill { get; set; }
        public string totalInWords { get; set; }
        public Nullable<double> cgst { get; set; }
        public Nullable<double> sgst { get; set; }
        public Nullable<double> mainTotal { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string modifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}