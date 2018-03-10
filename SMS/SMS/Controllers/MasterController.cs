using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMS.Models;
using SMS.DAL;

namespace SMS.Controllers
{

    public class MasterController : Controller
    {
        MasterDAL masterDal = new MasterDAL();
       [LoginCheckAttribute]
        public ActionResult Index()
        {
            return View();
        }
        #region Party
        [LoginCheckAttribute]
        public ActionResult Party()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAllParties()
        {
            List<PartyDetails> partyDetails = new List<PartyDetails>();
            partyDetails = masterDal.ReadPartyDetails();
            return Json(partyDetails, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveParties(string partyId, string partyType, string partyName, string mobile, string telephone, string address, string city, string pincode, string state, string country, string email, string pancard, string gstIn)
        {
            if (partyId == "" || partyId == null)
                partyId = "0";
            if (telephone == "" || telephone == null)
                telephone = "0";
            if (pincode == "" || pincode == null)
                pincode = "0";

            Response res=new Response();
            List<object> resultList = new List<object>();
            var partyFlag = Int32.Parse(partyId);
            PartyDetails party = new PartyDetails();
            party.partyType = partyType;
            party.partyName=partyName;
            party.mobile = long.Parse(mobile);
            party.telephone=long.Parse(telephone);
            party.address=address;
            party.city=city;
            party.pincode=Int32.Parse(pincode);
            party.state=state;
            party.country=country;
            party.email=email;
            party.pancard=pancard;
            party.gstIn = gstIn;
            party.partyId=Int32.Parse(partyId);

            if (partyFlag == 0)
                res = masterDal.SavePartyDetails(party);
            else
                res = masterDal.UpdatePartyDetails(party);
                resultList.Add(res);
                resultList.Add(GetAllParties());


                return Json(resultList, JsonRequestBehavior.AllowGet);
        }
         [HttpPost]
        public ActionResult DeleteParties(string partyId)
        {
            Response res = new Response();
            List<object> resultList = new List<object>();
            res = masterDal.DeletePartyDetails(Int32.Parse(partyId));
            resultList.Add(res);
            resultList.Add(GetAllParties());
            return Json(resultList, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Vehicle
        [LoginCheckAttribute]
         public ActionResult Vehicle()
         {
             return View();
         }
         [HttpPost]
         public ActionResult GetAllVehicles()
         {
             List<object> resultList = new List<object>();
             List<VehicleDetails> vehicleDetails = new List<VehicleDetails>();
             vehicleDetails = masterDal.ReadVehicleDetails();
             resultList.Add(vehicleDetails);
             resultList.Add(GetPartiesSelect());
             return Json(resultList, JsonRequestBehavior.AllowGet);
         }
        [HttpPost]
         public ActionResult GetPartiesSelect()
         {
             List<PartySelect> partyDetails = new List<PartySelect>();
             var partSelectdetails = masterDal.ReadPartyDetails();
             foreach (var data in partSelectdetails) { 
                PartySelect psObj= new PartySelect();
                 psObj.partyId=data.partyId;
                psObj.partyName=data.partyName;
                partyDetails.Add(psObj);
             }
             return Json(partyDetails, JsonRequestBehavior.AllowGet);
         }
         [HttpPost]
         public ActionResult SaveVehicles(string vehicleId, string partyId, string vehicleType, string vehicleNo, string tareWeight)
         {
             if (vehicleId == "" || vehicleId == null)
                 vehicleId = "0";
             Response res = new Response();
             List<object> resultList = new List<object>();
             var vehicleFlag = Int32.Parse(vehicleId);
             VehicleDetails vehicle = new VehicleDetails();
             vehicle.vehicleId = Int32.Parse(vehicleId);
             vehicle.partyId = Int32.Parse(partyId);
             vehicle.vehicleType = vehicleType;
             vehicle.vehicleNo = vehicleNo;
             vehicle.tareWeight = float.Parse(tareWeight);


             if (vehicleFlag == 0)
                 res = masterDal.SaveVehicleDetails(vehicle);
             else
                 res = masterDal.UpdateVehicleDetails(vehicle);
             resultList.Add(res);
             resultList.Add(GetAllVehicles());


             return Json(resultList, JsonRequestBehavior.AllowGet);
         }

         [HttpPost]
         public ActionResult DeleteVehicles(string vehicleId)
         {
             Response res = new Response();
             List<object> resultList = new List<object>();
             res = masterDal.DeleteVehicleDetails(Int32.Parse(vehicleId));
             resultList.Add(res);
             resultList.Add(GetAllVehicles());
             return Json(resultList, JsonRequestBehavior.AllowGet);

         }
        #endregion
        #region Item
        [LoginCheckAttribute]
         public ActionResult Item()
         {
             return View();
         }
         [HttpPost]
         public ActionResult GetAllItems()
         {
             List<ItemDetails> itemDetails = new List<ItemDetails>();
             itemDetails = masterDal.ReadItemDetails();
             return Json(itemDetails, JsonRequestBehavior.AllowGet);
         }
         [HttpPost]
         public ActionResult SaveItems(string materialId, string material, string rate)
         {
             if (materialId == "" || materialId == null)
                 materialId = "0";
             Response res = new Response();
             List<object> resultList = new List<object>();
             var materialFlag = Int32.Parse(materialId);
             ItemDetails item = new ItemDetails();
             item.materialId = Int32.Parse(materialId);
             item.material = material;
             item.materialId = materialFlag;
             item.rate = float.Parse(rate);
             if (materialFlag == 0)
                 res = masterDal.SaveItemDetails(item);
             else
                 res = masterDal.UpdateItemDetails(item);
             resultList.Add(res);
             resultList.Add(GetAllItems());


             return Json(resultList, JsonRequestBehavior.AllowGet);
         }
         [HttpPost]
         public ActionResult DeleteItems(string materialId)
         {
             Response res = new Response();
             List<object> resultList = new List<object>();
             res = masterDal.DeleteItemDetails(Int32.Parse(materialId));
             resultList.Add(res);
             resultList.Add(GetAllItems());
             return Json(resultList, JsonRequestBehavior.AllowGet);

         }
        #endregion
        #region users
        [LoginCheckAttribute]
         public ActionResult User()
         {
             return View();
         }
        [HttpPost]
         public ActionResult GetAllUsers()
         {
             List<UserDetails> userDetails = new List<UserDetails>();
             userDetails = masterDal.ReadUserDetails().OrderByDescending(x=>x.id).ToList();
             return Json(userDetails, JsonRequestBehavior.AllowGet);
         }
        [HttpPost]
        public ActionResult SaveUsers(string id, string employeeName, string username, string password, string role)
        {
            if (id == "" || id == null)
                id = "0";
            Response res = new Response();
            List<object> resultList = new List<object>();
            var idFlag = Int32.Parse(id);
            UserDetails item = new UserDetails();
            item.employeeName = employeeName;
            item.username = username;
            item.password = password;
            item.role = role;
            item.id = idFlag;
            if (idFlag == 0)
                res = masterDal.SaveUserDetails(item);
            else
                res = masterDal.UpdateUserDetails(item);
            resultList.Add(res);
            resultList.Add(GetAllUsers());


            return Json(resultList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteUsers(string id)
        {
            Response res = new Response();
            List<object> resultList = new List<object>();
            res = masterDal.DeleteUserDetails(Int32.Parse(id));
            resultList.Add(res);
            resultList.Add(GetAllUsers());
            return Json(resultList, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region Customer
        [LoginCheckAttribute]
        public ActionResult Customer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAllCustomers()
        {
            List<CustomerDetails> itemDetails = new List<CustomerDetails>();
            itemDetails = masterDal.ReadCustomerDetails();
            return Json(itemDetails, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SaveCustomers(string customerId, string customerName, string email, string mobile)
        {
            if (customerId == "" || customerId == null)
                customerId = "0";
            Response res = new Response();
            List<object> resultList = new List<object>();
            var customerFlag = Int32.Parse(customerId);
            CustomerDetails item = new CustomerDetails();
            item.customerName = customerName;
            item.email = email;
            item.mobile = mobile;
            item.customerId = customerFlag;
            if (customerFlag == 0)
                res = masterDal.SaveCustomerDetails(item);
            else
                res = masterDal.UpdateCustomerDetails(item);
            resultList.Add(res);
            resultList.Add(GetAllCustomers());


            return Json(resultList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteCustomers(string customerId)
        {
            Response res = new Response();
            List<object> resultList = new List<object>();
            res = masterDal.DeleteCustomerDetails(Int32.Parse(customerId));
            resultList.Add(res);
            resultList.Add(GetAllCustomers());
            return Json(resultList, JsonRequestBehavior.AllowGet);

        }
        #endregion
       
    }
}