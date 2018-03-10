using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public class MaterialReport
    {
        public long materialId { get; set; }
        public string material { get; set; }
        public Nullable<double> rate { get; set; }
        public Nullable<double> weight { get; set; }
        public Nullable<double> total { get; set; }

    }
    public class CustomerReport
    {
        public long NumberOfLoad { get; set; }
        public string CustomerName { get; set; }
        public string Place { get; set; }
        public string Material { get; set; }
        public Nullable<double> rate { get; set; }
        public Nullable<double> weight { get; set; }
        public Nullable<double> total { get; set; }
    }
}