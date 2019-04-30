using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public class WeighmentDetails
    {
        public long serialNo { get; set; }
        public string date { get; set; }
        public Nullable<System.DateTime> dateReal { get; set; }
        public int timeReal { get; set; }
        public string dateStr { get; set; }
        public string weighmentType { get; set; }
        public string salesMode { get; set; }
        public string unit { get; set; }
        public string vehicleNo { get; set; }
        public string driverName { get; set; }
        public string customerName { get; set; }
        public string placeOfDelivery { get; set; }
        public string loadType { get; set; }
        public string material { get; set; }
        public Nullable<double> grossWeight { get; set; }
        public Nullable<double> tareWeight { get; set; }
        public Nullable<double> netWeight { get; set; }
        public Nullable<double> rate { get; set; }
        public Nullable<double> tax { get; set; }
        public Nullable<double> rent { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<double> netAmount { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string updatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }
    }
}