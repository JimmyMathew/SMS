//using SMS.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace SMS.DAL
//{
//    public class BillDAL : Controller
//    {
//        smsEntities entities = new smsEntities();
//        public List<billDetails> ReadBillDetails() {
//            return entities.bills.Select(x => new billDetails
//            {
//                billNo=x.billNo,
//                dateBill=x.dateBill,
//                toName=x.toName.ToString(),
//                addLine1=x.addLine1.ToString(),
//                addLine2 = x.addLine2.ToString(),
//                addLine3 = x.addLine3.ToString(),
//                transportMode=x.transportMode.ToString(),
//                vehicleNoBill=x.vehicleNoBill.ToString(),
//                placeOfDeliveryBill=x.placeOfDeliveryBill.ToString(),
//                materialDescription=x.materialDescription.ToString(),
//                netWeightQty=x.netWeightQty,
//                amountBill=x.amountBill,
//                totalAmountBill=x.totalAmountBill,
//                cgst=x.cgst,
//                sgst=x.sgst,
//                mainTotal=x.mainTotal

//            }).OrderByDescending(x => x.billNo).ToList();
//        }
//        public Response SaveBillDetails(billDetails obj)
//        {
//            List<billDetails> weighList = new List<billDetails>();
//            var currentUser = System.Web.HttpContext.Current.Session["employeeName"].ToString();
//            bill billObj = new bill();
//            billObj.dateBill = obj.dateBill;
//            billObj.toName = obj.toName;
//            billObj.addLine1 = obj.addLine1;
//            billObj.addLine2 = obj.addLine2;
//            billObj.addLine3 = obj.addLine3;
//            billObj.transportMode = obj.transportMode;
//            billObj.vehicleNoBill = obj.vehicleNoBill;
//            billObj.placeOfDeliveryBill = obj.placeOfDeliveryBill;
//            billObj.materialDescription = obj.materialDescription;
//            billObj.netWeightQty = obj.netWeightQty;
//            billObj.amountBill = obj.amountBill;
//            billObj.rateBill = obj.rateBill;
//            billObj.totalInWords = obj.totalInWords;
//            billObj.cgst = obj.cgst;
//            billObj.sgst = obj.sgst;
//            billObj.mainTotal = obj.mainTotal;
//            billObj.createdBy = currentUser;
//            billObj.createdOn = DateTime.Now;
//            billObj.modifiedBy = currentUser;
//            billObj.ModifiedOn = DateTime.Now;
//            entities.bills.Add(billObj);
//            entities.SaveChanges();
//            long id = billObj.billNo;
//            return new Response { IsSuccess = true, Message = "Bill details added", ReturnId = id };
//        }
//        public Response UpdateBillDetails(billDetails obj)
//        {
//            var currentUser = System.Web.HttpContext.Current.Session["employeeName"].ToString();
//            var IsExist = entities.bills.Where(x => x.billNo == obj.billNo).ToList();
//            if (IsExist.Count != 0)
//            {
//                var billObj = entities.bills.Where(x => x.billNo == obj.billNo).FirstOrDefault();
//                billObj.dateBill = obj.dateBill;
//                billObj.toName = obj.toName;
//                billObj.addLine1 = obj.addLine1;
//                billObj.addLine2 = obj.addLine2;
//                billObj.addLine3 = obj.addLine3;
//                billObj.transportMode = obj.transportMode;
//                billObj.vehicleNoBill = obj.vehicleNoBill;
//                billObj.placeOfDeliveryBill = obj.placeOfDeliveryBill;
//                billObj.materialDescription = obj.materialDescription;
//                billObj.netWeightQty = obj.netWeightQty;
//                billObj.amountBill = obj.amountBill;
//                billObj.totalAmountBill = obj.totalAmountBill;
//                billObj.cgst = obj.cgst;
//                billObj.sgst = obj.sgst;
//                billObj.mainTotal = obj.mainTotal;
//                //billObj.createdBy = currentUser;
//                //billObj.createdOn = DateTime.Now;
//                billObj.modifiedBy = currentUser;
//                billObj.ModifiedOn = DateTime.Now;
//                entities.SaveChanges();
//                long id = billObj.billNo;
//                return new Response { IsSuccess = true, Message = "Bill details Updated", ReturnId = id };
//            }
//            else
//                return new Response { IsSuccess = false, Message = "Error" };


//        }
//        public Response DeleteBillDetails(int billNo)
//        {
//            var itemObj = entities.bills.Where(x => x.billNo == billNo).FirstOrDefault();
//            if (itemObj != null)
//            {
//                entities.bills.Remove(itemObj);
//                entities.SaveChanges();
//                return new Response { IsSuccess = false, Message = "Data deleted successfully" };
//            }
//            else
//                return new Response { IsSuccess = false, Message = "Data deletion error. Contact Administrator" };
//        }
//    }
//}