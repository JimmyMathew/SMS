using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMS.Models;
using SMS.DAL;
using System.Globalization;
using System.IO.Ports;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
namespace SMS.Controllers
{
    public class WeighmentController : Controller
    {
        WeighmentDAL weighDAL = new WeighmentDAL();
        MasterDAL materDAL = new MasterDAL();
        SerialPort serialPort;
        
        
       #region Weighment
        [LoginCheckAttribute]
        public ActionResult Index()
        {
            //SetSerialPortConfig();
            return View();
        }
       [HttpPost]
        public ActionResult ReadBilledvehicle(string serialNo)
         {
             int sNO = int.Parse(serialNo);
             List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
             weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.serialNo == sNO).OrderByDescending(x => x.serialNo).ToList();
             return Json(weighDetails, JsonRequestBehavior.AllowGet);
         }
        [HttpPost]
        public ActionResult SaveWeighments(string serialNo, string date, string weighmentType, string salesMode, string unit, string vehicleNo, string driverName, string customerName, string placeOfDelivery, string loadType, string material, string grossWeight, string tareWeight, string netWeight, string rate, string tax, string rent, string amount, string netAmount)
        {
            if (serialNo == "" || serialNo == null)
                serialNo = "0";
            if(grossWeight=="" || grossWeight==null)
                grossWeight = "0";
            if (tareWeight == "" || tareWeight == null)
                tareWeight = "0";
            if (netWeight == "" || netWeight == null)
                netWeight = "0";
            if (tax == "" || tax == null)
                tax = "0";
                if (rate == "" || rate == null)
                rate = "0";
                if (rent == "" || rent == null)
                    rent = "0";
                if (amount == "" || amount == null)
                    amount = "0";
                if (netAmount == "" || netAmount == null)
                    netAmount = "0";
            
            Response res = new Response();
            List<object> resultList = new List<object>();
            var serialFlag = Int32.Parse(serialNo);
            WeighmentDetails weight = new WeighmentDetails();
            weight.serialNo = serialFlag;
            weight.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
             weight.weighmentType=weighmentType;
             weight.salesMode=salesMode;
             weight.unit=unit;
             weight.vehicleNo=vehicleNo;
             weight.driverName=driverName;
             weight.customerName=customerName;
             weight.placeOfDelivery = placeOfDelivery;
             weight.loadType=loadType;
             weight.material=material;
             weight.grossWeight=float.Parse(grossWeight);
             weight.tareWeight = float.Parse(tareWeight);
             weight.netWeight = float.Parse(netWeight);
             weight.rate = float.Parse(rate);
             weight.tax = float.Parse(tax);
             weight.rent = float.Parse(rent);
             weight.amount = float.Parse(amount);
             weight.netAmount = float.Parse(netAmount);
            if (serialFlag == 0)
                res = weighDAL.SaveWeighmentDetails(weight);
            else
                res = weighDAL.UpdateWeighmentDetails(weight);
           resultList.Add(res);
           return Json(resultList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetSelectVehicleDetails(string vehicleNo)
        {
            List<object> resultList = new List<object>();
           var vehicleDetails= materDAL.ReadVehicleDetails();
           var item = vehicleDetails.SingleOrDefault(x => x.vehicleNo == vehicleNo);
           if (item != null)
           {
               resultList.Add(item.tareWeight);
               resultList.Add(item.partyName);
           }
           else
               resultList.Add(0);
            return Json(resultList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetSelectMaterialDetails(string material)
        {
            List<object> resultList = new List<object>();
            var materialDetails = materDAL.ReadItemDetails();
            var item = materialDetails.SingleOrDefault(x => x.material == material);
            if (item != null)
                resultList.Add(item.rate);
            else
                resultList.Add(0);
            return Json(resultList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetMasterData()
        {
            List<object> resultList = new List<object>();
            var vehicleDetails = materDAL.ReadVehicleDetails();
            resultList.Add(vehicleDetails);
            var partyDetails = materDAL.ReadPartyDetails();
            var customerDetails = materDAL.ReadCustomerDetails();
            resultList.Add(partyDetails);
            resultList.Add(customerDetails);
            var weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Empty").OrderByDescending(x => x.serialNo).ToList();
            resultList.Add(weighDetails);
            var itemDetails = materDAL.ReadItemDetails().ToList();
            resultList.Add(itemDetails);
            return Json(resultList, JsonRequestBehavior.AllowGet);
        }
       
#endregion
       #region Hitory
        [LoginCheckAttribute]
        public ActionResult History()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetHistory()
        {
            //Jimmy Mathew::Adding date functionality to History::10032018
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string[] space = today.Split(' ');
            string[] todaydatearr = space[0].Split('-');
            DateTime todayDateReal = new DateTime(Int32.Parse(todaydatearr[0]), Int32.Parse(todaydatearr[1]), Int32.Parse(todaydatearr[2]));
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithdate = new List<WeighmentDetails>();
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load").OrderByDescending(x => x.serialNo).ToList();
            foreach (var d in weighDetails)
            {
                string[] spaceDate = d.date.Split(' ');
                string[] datearr = spaceDate[0].Split('-');
                DateTime datReal = new DateTime(Int32.Parse(datearr[0]), Int32.Parse(datearr[1]), Int32.Parse(datearr[2]));
                d.dateReal = datReal;
                weighDetailsWithdate.Add(d);
            }
            var datewiseResult = weighDetailsWithdate.Where(x => x.dateReal == todayDateReal).OrderByDescending(x => x.serialNo).ToList();

            return Json(datewiseResult, JsonRequestBehavior.AllowGet);
        }

        //Jimmy Mathew::Adding date functionality to History::10032018
        [HttpPost]
        public ActionResult GetDateWiseHistory(string fromDate, string toDate)
        {
            string[] fromdatearr = fromDate.Split('-');
            string[] todatearr = toDate.Split('-');
            DateTime fromDateReal = new DateTime(Int32.Parse(fromdatearr[0]), Int32.Parse(fromdatearr[1]), Int32.Parse(fromdatearr[2]));
            DateTime toDateReal = new DateTime(Int32.Parse(todatearr[0]), Int32.Parse(todatearr[1]), Int32.Parse(todatearr[2]));
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithdate = new List<WeighmentDetails>();
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load").OrderByDescending(x => x.serialNo).ToList();
            foreach (var d in weighDetails)
            {
                string[] spaceDate = d.date.Split(' ');
                string[] datearr = spaceDate[0].Split('-');
                DateTime datReal = new DateTime(Int32.Parse(datearr[0]), Int32.Parse(datearr[1]), Int32.Parse(datearr[2]));
                d.dateReal = datReal;
                weighDetailsWithdate.Add(d);
            }

            List<WeighmentDetails> datewiseResult = new List<WeighmentDetails>();
            datewiseResult = weighDetailsWithdate.Where(x => x.dateReal >= fromDateReal && x.dateReal <= toDateReal).OrderByDescending(x => x.serialNo).ToList();
            //Jimmy Mathew::History data load bug fix::10032018
            var jsonResult = Json(datewiseResult,JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
#endregion
        #region EmptyList
        [LoginCheckAttribute]
        public ActionResult EmptyList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetEmptyList()
        {
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Empty").OrderByDescending(x => x.serialNo).ToList();
            return Json(weighDetails, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteEmpty(string serialNo)
        {
            Response res = new Response();
            List<object> resultList = new List<object>();
            res = weighDAL.DeleteWeighmentDetails(Int32.Parse(serialNo));
            resultList.Add(res);
            resultList.Add(GetEmptyList());
            return Json(resultList, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Reports
        [LoginCheckAttribute]
        public ActionResult Report()
        {
            return View();
        }
        [LoginCheckAttribute]
        public ActionResult SalesReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SalesReportGenerationToday()
        {
            
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string[] space = today.Split(' ');
            string[] todaydatearr = space[0].Split('-');
            DateTime todayDateReal = new DateTime(Int32.Parse(todaydatearr[0]), Int32.Parse(todaydatearr[1]), Int32.Parse(todaydatearr[2]));
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithdate = new List<WeighmentDetails>();
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load" && x.weighmentType=="Sales").ToList();
            foreach (var d in weighDetails)
            {
                string[] spaceDate = d.date.Split(' ');
                string[] datearr = spaceDate[0].Split('-');
                DateTime datReal = new DateTime(Int32.Parse(datearr[0]), Int32.Parse(datearr[1]), Int32.Parse(datearr[2]));
                d.dateReal = datReal;
                weighDetailsWithdate.Add(d);
            }
            var datewiseResult = weighDetailsWithdate.Where(x => x.dateReal == todayDateReal).OrderByDescending(x => x.serialNo).ToList();
            return Json(datewiseResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalesReportTimeBased( string fromTime,string toTime) {
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string[] space = today.Split(' ');
            string[] todaydatearr = space[0].Split('-');
            DateTime todayDateReal = new DateTime(Int32.Parse(todaydatearr[0]), Int32.Parse(todaydatearr[1]), Int32.Parse(todaydatearr[2]));
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithdate = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithTime = new List<WeighmentDetails>();
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load" && x.weighmentType == "Sales").ToList();
            foreach (var d in weighDetails)
            {
                string[] spaceDate = d.date.Split(' ');
                string[] datearr = spaceDate[0].Split('-');
                DateTime datReal = new DateTime(Int32.Parse(datearr[0]), Int32.Parse(datearr[1]), Int32.Parse(datearr[2]));
                d.dateReal = datReal;
                weighDetailsWithdate.Add(d);
            }
            var datewiseResult = weighDetailsWithdate.Where(x => x.dateReal == todayDateReal).ToList();

            foreach (var t in datewiseResult)
            {
                string[] spaceTime= t.date.Split(' ');
                string[] datearr = spaceTime[1].Split(':');
                t.timeReal = Int32.Parse(datearr[0]);
                weighDetailsWithTime.Add(t);

            }
            var timewiseResult = weighDetailsWithTime.Where(x => x.timeReal >= Int32.Parse(fromTime) && x.timeReal <= Int32.Parse(toTime)).OrderByDescending(x => x.serialNo).ToList();
            return Json(timewiseResult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PurchaseReportTimeBased(string fromTime, string toTime)
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string[] space = today.Split(' ');
            string[] todaydatearr = space[0].Split('-');
            DateTime todayDateReal = new DateTime(Int32.Parse(todaydatearr[0]), Int32.Parse(todaydatearr[1]), Int32.Parse(todaydatearr[2]));
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithdate = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithTime = new List<WeighmentDetails>();
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load" && x.weighmentType == "Purchase").ToList();
            foreach (var d in weighDetails)
            {
                string[] spaceDate = d.date.Split(' ');
                string[] datearr = spaceDate[0].Split('-');
                DateTime datReal = new DateTime(Int32.Parse(datearr[0]), Int32.Parse(datearr[1]), Int32.Parse(datearr[2]));
                d.dateReal = datReal;
                weighDetailsWithdate.Add(d);
            }
            var datewiseResult = weighDetailsWithdate.Where(x => x.dateReal == todayDateReal).ToList();

            foreach (var t in datewiseResult)
            {
                string[] spaceTime = t.date.Split(' ');
                string[] datearr = spaceTime[1].Split(':');
                t.timeReal = Int32.Parse(datearr[0]);
                weighDetailsWithTime.Add(t);

            }
            var timewiseResult = weighDetailsWithTime.Where(x => x.timeReal >= Int32.Parse(fromTime) && x.timeReal <= Int32.Parse(toTime)).OrderByDescending(x => x.serialNo).ToList();
            return Json(timewiseResult, JsonRequestBehavior.AllowGet);
        }
        [LoginCheckAttribute]
        public ActionResult PurchaseReport()
        {
            return View();
        }
        public ActionResult PurchaseReportGenerationToday()
        {

            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string[] space = today.Split(' ');
            string[] todaydatearr = space[0].Split('-');
            DateTime todayDateReal = new DateTime(Int32.Parse(todaydatearr[0]), Int32.Parse(todaydatearr[1]), Int32.Parse(todaydatearr[2]));
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithdate = new List<WeighmentDetails>();
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load" && x.weighmentType == "Purchase").ToList();
            foreach (var d in weighDetails)
            {
                string[] spaceDate = d.date.Split(' ');
                string[] datearr = spaceDate[0].Split('-');
                DateTime datReal = new DateTime(Int32.Parse(datearr[0]), Int32.Parse(datearr[1]), Int32.Parse(datearr[2]));
                d.dateReal = datReal;
                weighDetailsWithdate.Add(d);
            }
            var datewiseResult = weighDetailsWithdate.Where(x => x.dateReal == todayDateReal).OrderByDescending(x => x.serialNo).ToList();
            return Json(datewiseResult, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ReportGenerationEmpty()
        {
            List<object> resultList = new List<object>();
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            resultList.Add(weighDetails);
            var itemDetails = materDAL.ReadItemDetails().ToList();
            resultList.Add(itemDetails);
            var customerList = materDAL.ReadCustomerDetails().Select(l=>l.customerName).ToList();
            var partyList = materDAL.ReadPartyDetails().Select(l => l.partyName).ToList();
            var finalList = customerList.Concat(partyList).ToList();
            resultList.Add(finalList);

            return Json(resultList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ReportGenerationAll()
        {
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load").OrderByDescending(x => x.serialNo).ToList();
            return Json(weighDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ReportDateWiseGeneration(string fromDate, string toDate,string material,string customer)
        {
            
            string[] fromdatearr = fromDate.Split('-');
            string[] todatearr = toDate.Split('-');
            DateTime fromDateReal = new DateTime(Int32.Parse(fromdatearr[0]), Int32.Parse(fromdatearr[1]), Int32.Parse(fromdatearr[2]));
            DateTime toDateReal = new DateTime(Int32.Parse(todatearr[0]), Int32.Parse(todatearr[1]), Int32.Parse(todatearr[2]));
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithdate = new List<WeighmentDetails>();
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load").ToList();
            foreach (var d in weighDetails) {
                string[] spaceDate = d.date.Split(' ');
                string[] datearr = spaceDate[0].Split('-');
                DateTime datReal = new DateTime(Int32.Parse(datearr[0]), Int32.Parse(datearr[1]), Int32.Parse(datearr[2]));
                d.dateReal = datReal;
                weighDetailsWithdate.Add(d);
            }
            List<WeighmentDetails>datewiseResult=new List<WeighmentDetails>();
            if(material=="All" && customer=="")
             datewiseResult = weighDetailsWithdate.Where(x => x.dateReal >= fromDateReal && x.dateReal <= toDateReal).OrderByDescending(x => x.serialNo).ToList();
            else if (material != "All" && customer=="")
                datewiseResult = weighDetailsWithdate.Where(x => x.dateReal >= fromDateReal && x.dateReal <= toDateReal && x.material == material).OrderByDescending(x => x.serialNo).ToList();
            else if (material == "All" && customer != "")
                datewiseResult = weighDetailsWithdate.Where(x => x.dateReal >= fromDateReal && x.dateReal <= toDateReal && x.customerName == customer).OrderByDescending(x => x.serialNo).ToList();
            else
                datewiseResult = weighDetailsWithdate.Where(x => x.dateReal >= fromDateReal && x.dateReal <= toDateReal && x.customerName == customer && x.material==material).OrderByDescending(x => x.serialNo).ToList();
            return Json(datewiseResult, JsonRequestBehavior.AllowGet);
        }
        [LoginCheckAttribute]
        public ActionResult MaterialReport()
        {
            return View();
        }
            [HttpPost]
             public ActionResult GetMaterialReportEmpty(){
             List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
              return Json(weighDetails, JsonRequestBehavior.AllowGet);
             }
    
        [HttpPost]
        public ActionResult GetMaterialReport(string weighmentType)
        {
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string[] space = today.Split(' ');
            string[] todaydatearr = space[0].Split('-');
            DateTime todayDateReal = new DateTime(Int32.Parse(todaydatearr[0]), Int32.Parse(todaydatearr[1]), Int32.Parse(todaydatearr[2]));
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithdate = new List<WeighmentDetails>();
            if (weighmentType=="Sales")
            weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load" && x.weighmentType=="Sales").ToList();
            else
                weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load" && x.weighmentType == "Purchase").ToList();
            foreach (var d in weighDetails)
            {
                string[] spaceDate = d.date.Split(' ');
                string[] datearr = spaceDate[0].Split('-');
                DateTime datReal = new DateTime(Int32.Parse(datearr[0]), Int32.Parse(datearr[1]), Int32.Parse(datearr[2]));
                d.dateReal = datReal;
                weighDetailsWithdate.Add(d);
            }
            var datewiseResult = weighDetailsWithdate.Where(x => x.dateReal == todayDateReal).ToList();
            List<MaterialReport> result = datewiseResult
                 .GroupBy(l => l.material)
                .Select(cl => new MaterialReport
                  {
                        material=cl.First().material,
                        rate=cl.First().rate,
                        weight=cl.Sum(c=>c.netWeight),
                        total= cl.Sum(c=>c.netAmount)
                 }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [LoginCheckAttribute]
        public ActionResult CustomerReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetCustomerReportEmpty()
        {
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            return Json(weighDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCustomerReport(string weighmentType, string fromDate)
        {
            Nullable<DateTime> fromDateReal = null;
             List<WeighmentDetails>  datewiseResult = new List<WeighmentDetails>();
           
                string[] fromdatearr = fromDate.Split('-');
                 fromDateReal = new DateTime(Int32.Parse(fromdatearr[0]), Int32.Parse(fromdatearr[1]), Int32.Parse(fromdatearr[2]));
           
            var today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string[] space = today.Split(' ');
            string[] todaydatearr = space[0].Split('-');
            DateTime todayDateReal = new DateTime(Int32.Parse(todaydatearr[0]), Int32.Parse(todaydatearr[1]), Int32.Parse(todaydatearr[2]));
            List<WeighmentDetails> weighDetails = new List<WeighmentDetails>();
            List<WeighmentDetails> weighDetailsWithdate = new List<WeighmentDetails>();
            if (weighmentType == "Sales")
                weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load" && x.weighmentType == "Sales").ToList();
            else
                weighDetails = weighDAL.ReadWeighmentDetails().Where(x => x.loadType == "Full Load" && x.weighmentType == "Purchase").ToList();
            foreach (var d in weighDetails)
            {
                string[] spaceDate = d.date.Split(' ');
                string[] datearr = spaceDate[0].Split('-');
                DateTime datReal = new DateTime(Int32.Parse(datearr[0]), Int32.Parse(datearr[1]), Int32.Parse(datearr[2]));
                d.dateReal = datReal;
                weighDetailsWithdate.Add(d);
            }
             datewiseResult = weighDetailsWithdate.Where(x => x.dateReal == fromDateReal).ToList();
            List<CustomerReport> result = datewiseResult
                 .GroupBy(l => l.customerName)
                .Select(cl => new CustomerReport
                {
                    CustomerName=cl.First().customerName,
                    NumberOfLoad=cl.Count(),
                    Place = cl.First().placeOfDelivery,
                    weight = cl.Sum(c => c.netWeight),
                    total = cl.Sum(c => c.netAmount)
                }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Serial Port 
        public ActionResult ScanGrossWeight()
        {
            var weight = SetSerialPortConfig();
            return Json(weight, JsonRequestBehavior.AllowGet);
        }
        public List<ComDetails> GetSerialPortConfig()
        {

            List<ComDetails> comDetails = new List<ComDetails>();
            comDetails = materDAL.ReadComDetails().ToList();
            return comDetails;
        }
        public string SetSerialPortConfig() {
            string output = "";
           //Serial port data read    
           List<ComDetails> comDetails = new List<ComDetails>();
           if (serialPort != null)
           {
               if (serialPort.IsOpen)
                   serialPort.Close();

               serialPort.Dispose();
           }
           comDetails = GetSerialPortConfig();
           var port = comDetails[0].port;
           var buadRate = Int32.Parse(comDetails[0].buadRate);
           Parity parity = (Parity)Int32.Parse(comDetails[0].parity);
           var dataBit = Int32.Parse(comDetails[0].dataBit);
           StopBits stopBit = (StopBits)Int32.Parse(comDetails[0].stopBit);
           serialPort = new SerialPort(port,buadRate,parity,dataBit,stopBit);
           serialPort.DtrEnable = true;
           serialPort.RtsEnable = true;
           serialPort.ReadTimeout = 5000;
           serialPort.Open();
           List<string> buffer = new List<string>();
           for (var i = 0; i <= 10; i++) {
               buffer.Add(serialPort.ReadLine());
           }
           output = Regex.Replace(buffer[9], "[^0-9]+", string.Empty);
           output.TrimStart('0');
           output = output.Length > 0 ? output : "0";
           serialPort.Close();
           return output;

        }
        
        #endregion

    }
}