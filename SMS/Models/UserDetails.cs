using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public class UserDetails
    {
        public long id { get; set; }
        public string employeeName { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string createdBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}