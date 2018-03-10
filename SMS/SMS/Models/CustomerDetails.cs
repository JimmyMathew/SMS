using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public class CustomerDetails
    {
        public long customerId { get; set; }
        public string customerName { get; set; }
        public string partyName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
    }
}