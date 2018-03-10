using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.DAL
{
    public class WeighmentDAL : Controller
    {
        smsEntities entities = new smsEntities();
        

        public List<WeighmentDetails> ReadWeighmentDetails()
        {
            return entities.weighments.Select(x => new WeighmentDetails

              {

                  serialNo = x.serialNo,
                  date = x.date.ToString(),
                  dateStr = x.date.ToString(),
                  weighmentType = x.weighmentType,
                  salesMode = x.salesMode,
                  unit = x.unit,
                  vehicleNo = x.vehicleNo,
                  driverName = x.driverName,
                  customerName = x.customerName,
                  placeOfDelivery = x.placeOfDelivery,
                  loadType = x.loadType,
                  material = x.material,
                  grossWeight = x.grossWeight,
                  tareWeight = x.tareWeight,
                  netWeight = x.netWeight,
                  rate = x.rate,
                  tax = x.tax,
                  rent = x.rent,
                  amount = x.amount,
                  netAmount = x.netAmount,
                  createdBy = x.createdBy,
                  createdOn = x.createdOn,
                  updatedBy = x.updatedBy,
                  updatedOn = x.updatedOn


              }).OrderByDescending(x => x.serialNo).ToList();
        }
        public Response SaveWeighmentDetails(WeighmentDetails obj)
        {
            List<WeighmentDetails> weighList = new List<WeighmentDetails>();
            var currentUser = System.Web.HttpContext.Current.Session["employeeName"].ToString(); 
            weighment weighObj = new weighment();
            weighObj.date = obj.date;
            weighObj.weighmentType = obj.weighmentType;
            weighObj.salesMode = obj.salesMode;
            weighObj.unit = obj.unit;
            weighObj.vehicleNo = obj.vehicleNo;
            weighObj.driverName = obj.driverName;
            weighObj.customerName = obj.customerName;
            weighObj.placeOfDelivery = obj.placeOfDelivery;
            weighObj.loadType = obj.loadType;
            weighObj.material = obj.material;
            weighObj.grossWeight = obj.grossWeight;
            weighObj.tareWeight = obj.tareWeight;
            weighObj.netWeight = obj.netWeight;
            weighObj.rate = obj.rate;
            weighObj.tax = obj.tax;
            weighObj.rent = obj.rent;
            weighObj.amount = obj.amount;
            weighObj.netAmount = obj.netAmount;
            weighObj.createdBy = currentUser;
            weighObj.createdOn=DateTime.Now;
            weighObj.updatedBy = currentUser;
            weighObj.updatedOn = DateTime.Now;
            entities.weighments.Add(weighObj);
            
            entities.SaveChanges();
            long id = weighObj.serialNo;
            return new Response { IsSuccess = true, Message = "Empty weighment details added", ReturnId = id };
        }
        public Response UpdateWeighmentDetails(WeighmentDetails obj)
        {
            var currentUser = System.Web.HttpContext.Current.Session["employeeName"].ToString(); 
            var IsExist = entities.weighments.Where(x => x.serialNo == obj.serialNo).ToList();
            if (IsExist.Count != 0)
            {
                var weighObj = entities.weighments.Where(x => x.serialNo == obj.serialNo).FirstOrDefault();
                weighObj.date = obj.date;
                weighObj.weighmentType = obj.weighmentType;
                weighObj.salesMode = obj.salesMode;
                weighObj.unit = obj.unit;
                weighObj.vehicleNo = obj.vehicleNo;
                weighObj.driverName = obj.driverName;
                weighObj.customerName = obj.customerName;
                weighObj.placeOfDelivery = obj.placeOfDelivery;
                weighObj.loadType = obj.loadType;
                weighObj.material = obj.material;
                weighObj.grossWeight = obj.grossWeight;
                weighObj.tareWeight = obj.tareWeight;
                weighObj.netWeight = obj.netWeight;
                weighObj.rate = obj.rate;
                weighObj.tax = obj.tax;
                weighObj.rent = obj.rent;
                weighObj.amount = obj.amount;
                weighObj.netAmount = obj.netAmount;
                //weighObj.createdBy = currentUser;
                weighObj.updatedBy = currentUser;
                weighObj.updatedOn = DateTime.Now;
                entities.SaveChanges();
                long id = weighObj.serialNo;
                return new Response { IsSuccess = true, Message = "Weighment details Updated", ReturnId = id };
            }
            else
                return new Response { IsSuccess = false, Message = "Error" };
        

        }
        public Response DeleteWeighmentDetails(int serialNo)
        {
            var itemObj = entities.weighments.Where(x => x.serialNo == serialNo).FirstOrDefault();
            if (itemObj != null)
            {
                entities.weighments.Remove(itemObj);
                entities.SaveChanges();
                return new Response { IsSuccess = false, Message = "Data deleted successfully" };
            }
            else
                return new Response { IsSuccess = false, Message = "Data deletion error. Contact Administrator" };
        }

    }
}