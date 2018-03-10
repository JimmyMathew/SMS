using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public class ComDetails
    {
        public long id { get; set; }
        public string port { get; set; }
        public string buadRate { get; set; }
        public string parity { get; set; }
        public string dataBit { get; set; }
        public string stopBit { get; set; }
    }
}