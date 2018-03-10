using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public partial class PartyDetails
    {
        public long partyId { get; set; }
        public string partyName { get; set; }
        public string partyType { get; set; }
        public Nullable<long> mobile { get; set; }
        public Nullable<long> telephone { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public Nullable<int> pincode { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string pancard { get; set; }
        public string gstIn { get; set; }
        public string createBy { get; set; }
        public Nullable<System.DateTime> createdOn { get; set; }
        public string updatedBy { get; set; }
        public Nullable<System.DateTime> updatedOn { get; set; }

        public virtual ICollection<vehicle> vehicles { get; set; }
    }
    public class PartySelect
    {
        public long partyId { get; set; }
        public string partyName { get; set; }
    }
}