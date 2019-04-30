using SMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.DAL
{
    public class MasterDAL: Controller
    {
        smsEntities entities = new smsEntities();
        #region Party Details
        public List<PartyDetails> ReadPartyDetails()
        {
            return entities.party_details.Select(x => new PartyDetails
            {
              partyId =x.partyId,
              partyName =x.partyName,
              partyType=x.partyType,
              mobile =x.mobile,
              telephone =x.telephone,
              address =x.address,
              city =x.city,
              pincode =x.pincode,
              state =x.state,
              country=x.country,
              email =x.email,
              pancard=x.pancard,
              gstIn=x.gstIn,
              createBy=x.createBy,
              createdOn=x.createdOn,
              updatedBy=x.updatedBy,
              updatedOn=x.updatedOn

            }).OrderByDescending(x=>x.partyId).ToList();
        }

        public Response SavePartyDetails(PartyDetails obj) {
            List<PartyDetails> partyList = new List<PartyDetails>();
            var IsExist = entities.party_details.Where(x => x.partyName == obj.partyName && x.partyType == obj.partyType).ToList();
            if (IsExist.Count == 0)
            {
                party_details partyObj = new party_details();
                partyObj.partyType = obj.partyType;
                partyObj.partyName = obj.partyName;
                partyObj.mobile = obj.mobile;
                partyObj.telephone = obj.telephone;
                partyObj.address = obj.address;
                partyObj.city = obj.city;
                partyObj.pincode = obj.pincode;
                partyObj.state = obj.state;
                partyObj.country = obj.country;
                partyObj.email = obj.email;
                partyObj.pancard = obj.pancard;
                partyObj.partyId = obj.partyId;
                partyObj.gstIn = obj.gstIn;
                partyObj.createBy = "Admin";
                partyObj.createdOn = DateTime.Now;
                partyObj.updatedBy = "Admin";
                partyObj.updatedOn = DateTime.Now;
                entities.party_details.Add(partyObj);
                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Inserted " };
            }
            else
                return new Response { IsSuccess = false, Message = "Data already exists" };
 
        }
        public Response UpdatePartyDetails(PartyDetails obj)
        {
            List<PartyDetails> partyList = new List<PartyDetails>();
            var IsExist = entities.party_details.Where(x=>x.partyId==obj.partyId).ToList();
            if (IsExist.Count != 0)
            {
                var partyObj = entities.party_details.Where(x => x.partyId == obj.partyId).FirstOrDefault();
               
                partyObj.partyType = obj.partyType;
                partyObj.partyName = obj.partyName;
                partyObj.mobile = obj.mobile;
                partyObj.telephone = obj.telephone;
                partyObj.address = obj.address;
                partyObj.city = obj.city;
                partyObj.pincode = obj.pincode;
                partyObj.state = obj.state;
                partyObj.country = obj.country;
                partyObj.email = obj.email;
                partyObj.pancard = obj.pancard;
                partyObj.partyId = obj.partyId;
                partyObj.gstIn = obj.gstIn;
                partyObj.updatedBy = "Admin";
                partyObj.updatedOn = DateTime.Now;
                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Updated" };
            }
            else
                return new Response { IsSuccess = false, Message = "Updation Error. Contact Administrator" };

        }
        public Response DeletePartyDetails(int partyId)
        {
            var partyObj = entities.party_details.Where(x => x.partyId == partyId).FirstOrDefault();
            if (partyObj != null) {

                entities.party_details.Remove(partyObj);
                try
                {
                    entities.SaveChanges();
                }
                catch (Exception e)
                {
                    return new Response { IsSuccess = false, Message = "Cannot Delete.Vehicle is linked to this party." };
                    
                }
                return new Response { IsSuccess = false, Message = "Data deleted successfully" };
            }
            else
            return new Response { IsSuccess = false, Message = "Data deletion error" };
        }
        #endregion
        #region Vehicle Details
        public List<VehicleDetails> ReadVehicleDetails()
        {
            return entities.vehicles.Select(x => new VehicleDetails
            
            {
                vehicleId=x.vehicleId,
                partyId=x.party_details.partyId,
                partyName=x.party_details.partyName,
                vehicleType =x.vehicleType,
                vehicleNo=x.vehicleNo,
                tareWeight=x.tareWeight,
                
            }).OrderByDescending(x=>x.vehicleId).ToList();
        }
        public Response SaveVehicleDetails(VehicleDetails obj)
        {
            List<VehicleDetails> vehicleList = new List<VehicleDetails>();
            var IsExist = entities.vehicles.Where(x => x.vehicleNo == obj.vehicleNo).ToList();
            if (IsExist.Count == 0)
            {
                vehicle vehicleObj = new vehicle();
               vehicleObj.vehicleId=obj.vehicleId;
               vehicleObj.partyId = obj.partyId;
		       vehicleObj. vehicleType=obj.vehicleType;
		        vehicleObj.vehicleNo=obj.vehicleNo;
                vehicleObj.tareWeight = obj.tareWeight;
                vehicleObj.createdBy = "Admin";
                vehicleObj.createdOn = DateTime.Now;
                vehicleObj.updatedBy = "Admin";
                vehicleObj.updatedOn = DateTime.Now;
                entities.vehicles.Add(vehicleObj);
                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Inserted " };
            }
            else
                return new Response { IsSuccess = false, Message = "Data already exists" };

        }
        public Response UpdateVehicleDetails(VehicleDetails obj)
        {
            List<VehicleDetails> vehicleList = new List<VehicleDetails>();
            var IsExist = entities.vehicles.Where(x => x.vehicleId == obj.vehicleId).ToList();
            if (IsExist.Count != 0)
            {
                var vehicleObj = entities.vehicles.Where(x => x.vehicleId == obj.vehicleId).FirstOrDefault();
                vehicleObj.vehicleId = obj.vehicleId;
                vehicleObj.partyId = obj.partyId;
                vehicleObj.vehicleType = obj.vehicleType;
                vehicleObj.vehicleNo = obj.vehicleNo;
                vehicleObj.tareWeight = obj.tareWeight;
                vehicleObj.updatedBy = "Admin";
                vehicleObj.updatedOn = DateTime.Now;
                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Updated " };
            }
            else
                return new Response { IsSuccess = false, Message = "Updation Error. Contact Administrator" };

        }
        public Response DeleteVehicleDetails(int vehicleId)
        {
            var vehicleObj = entities.vehicles.Where(x => x.vehicleId == vehicleId).FirstOrDefault();
            if (vehicleObj != null)
            {
                entities.vehicles.Remove(vehicleObj);
                entities.SaveChanges();
                return new Response { IsSuccess = false, Message = "Data deleted successfully" };
            }
            else
                return new Response { IsSuccess = false, Message = "Data deletion error" };
        }
        #endregion
        #region Item Details
        public List<ItemDetails> ReadItemDetails()
        {
            return entities.items.Select(x => new ItemDetails

            {
                materialId = x.materialId,
                material = x.material,
                rate = x.rate,


            }).OrderByDescending(x => x.materialId).ToList();
        }
        public Response SaveItemDetails(ItemDetails obj)
        {
            List<ItemDetails> itemList = new List<ItemDetails>();
            var IsExist = entities.items.Where(x => x.material == obj.material).ToList();
            if (IsExist.Count == 0)
            {
                item itemObj = new item();
                itemObj.materialId = obj.materialId;
                itemObj.material = obj.material;
                itemObj.rate = obj.rate;
                itemObj.createdBy = "Admin";
                itemObj.createdOn = DateTime.Now;
                itemObj.updatedBy = "Admin";
                itemObj.updatedOn = DateTime.Now;
                entities.items.Add(itemObj);
                
                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Inserted " };
            }
            else
                return new Response { IsSuccess = false, Message = "Data already exists" };

        }
        public Response UpdateItemDetails(ItemDetails obj)
        {
            List<ItemDetails> itemList = new List<ItemDetails>();
            var IsExist = entities.items.Where(x => x.materialId == obj.materialId).ToList();
            if (IsExist.Count != 0)
            {
                var itemObj = entities.items.Where(x => x.materialId == obj.materialId).FirstOrDefault();
                itemObj.materialId = obj.materialId;
                itemObj.material = obj.material;
                itemObj.rate = obj.rate;
                itemObj.updatedBy = "Admin";
                itemObj.updatedOn = DateTime.Now;
                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Updated " };
            }
            else
                return new Response { IsSuccess = false, Message = "Updation Error. Contact Administrator" };

        }
        public Response DeleteItemDetails(int materialId)
        {
            var itemObj = entities.items.Where(x => x.materialId == materialId).FirstOrDefault();
            if (itemObj != null)
            {
                entities.items.Remove(itemObj);
                entities.SaveChanges();
                return new Response { IsSuccess = false, Message = "Data deleted successfully" };
            }
            else
                return new Response { IsSuccess = false, Message = "Data deletion error. Contact Administrator" };
        }
        #endregion
        #region User Details
        public List<UserDetails> ReadUserDetails()
        {
            return entities.users.Select(x => new UserDetails

            {
                id=x.id,
                employeeName=x.employeeName,
                username=x.username,
                password=x.password,
                role=x.role


            }).OrderByDescending(x => x.id).ToList();
        }
        public Response SaveUserDetails(UserDetails obj)
        {
            List<UserDetails> userList = new List<UserDetails>();
            var IsExist = entities.users.Where(x => x.username == obj.username).ToList();
            if (IsExist.Count == 0)
            {
                user itemObj = new user();
                itemObj.id = obj.id;
                itemObj.employeeName = obj.employeeName;
                itemObj.username = obj.username;
                itemObj.password = obj.password;
                itemObj.role = obj.role;
                itemObj.createdBy = "Admin";
                itemObj.CreatedOn = DateTime.Now;
                itemObj.UpdatedBy = "Admin";
                itemObj.UpdatedOn = DateTime.Now;
                entities.users.Add(itemObj);

                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Inserted " };
            }
            else
                return new Response { IsSuccess = false, Message = "Data already exists" };

        }
        public Response UpdateUserDetails(UserDetails obj)
        {
            List<UserDetails> userList = new List<UserDetails>();
            var IsExist = entities.users.Where(x => x.id == obj.id).ToList();
            if (IsExist.Count != 0)
            {
                var itemObj = entities.users.Where(x => x.id == obj.id).FirstOrDefault();
                itemObj.id = obj.id;
                itemObj.employeeName = obj.employeeName;
                itemObj.username = obj.username;
                itemObj.password = obj.password;
                itemObj.role = obj.role;
                itemObj.createdBy = "Admin";
                itemObj.CreatedOn = DateTime.Now;
                itemObj.UpdatedBy = "Admin";
                itemObj.UpdatedOn = DateTime.Now;
                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Updated " };
            }
            else
                return new Response { IsSuccess = false, Message = "Data already exists" };

        }
        public Response DeleteUserDetails(int id)
        {
            var itemObj = entities.users.Where(x => x.id == id).FirstOrDefault();
            if (itemObj != null)
            {
                entities.users.Remove(itemObj);
                entities.SaveChanges();
                return new Response { IsSuccess = false, Message = "Data deleted successfully" };
            }
            else
                return new Response { IsSuccess = false, Message = "Data deletion error. Contact Administrator" };
        }
        #endregion
        #region Customer Details
        public List<CustomerDetails> ReadCustomerDetails()
        {
            return entities.customers.Select(x => new CustomerDetails

            {
                customerId=x.customerId,
                customerName=x.customerName,
                partyName = x.customerName,
                email=x.email,
                mobile=x.mobile
            }).OrderByDescending(x => x.customerId).ToList();
        }
        public Response SaveCustomerDetails(CustomerDetails obj)
        {
            List<CustomerDetails> itemList = new List<CustomerDetails>();
            var IsExist = entities.customers.Where(x => x.customerName == obj.customerName).ToList();
            if (IsExist.Count == 0) 
            {
                customer itemObj = new customer();
                itemObj.customerId = obj.customerId;
                itemObj.customerName = obj.customerName;
                itemObj.email = obj.email;
                itemObj.mobile = obj.mobile;
                entities.customers.Add(itemObj);

                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Inserted " };
            }
            else
                return new Response { IsSuccess = false, Message = "Data already exists" };

        }
        public Response UpdateCustomerDetails(CustomerDetails obj)
        {
            List<CustomerDetails> itemList = new List<CustomerDetails>();
            var IsExist = entities.customers.Where(x => x.customerId == obj.customerId).ToList();
            if (IsExist.Count != 0)
            {
                customer itemObj = entities.customers.Where(x => x.customerId == obj.customerId).FirstOrDefault();
                itemObj.customerId = obj.customerId;
                itemObj.customerName = obj.customerName;
                itemObj.email = obj.email;
                itemObj.mobile = obj.mobile;
                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "Data Successfully Updated " };
            }
            else
                return new Response { IsSuccess = false, Message = "Updation Error. Contact Administrator" };

        }
        public Response DeleteCustomerDetails(int customerId)
        {
            var itemObj = entities.customers.Where(x => x.customerId == customerId).FirstOrDefault();
            if (itemObj != null)
            {
                entities.customers.Remove(itemObj);
                entities.SaveChanges();
                return new Response { IsSuccess = false, Message = "Data deleted successfully" };
            }
            else
                return new Response { IsSuccess = false, Message = "Data deletion error. Contact Administrator" };
        }
        #endregion
        #region Com
        public List<ComDetails> ReadComDetails()
        {
            return entities.comports.Select(x => new ComDetails

            {
                id=x.id,
                port=x.port,
                buadRate=x.buadRate,
                parity=x.parity,
                dataBit=x.dataBit,
                stopBit=x.stopBit

            }).OrderByDescending(x => x.id).ToList();
        }
        public Response UpdateComDetails(ComDetails obj)
        {
            List<ComDetails> itemList = new List<ComDetails>();
            var IsExist = entities.comports.Where(x => x.id == obj.id).ToList();
            if (IsExist.Count != 0)
            {
                comport itemObj = entities.comports.Where(x => x.id == obj.id).FirstOrDefault();
                itemObj.port = obj.port;
                itemObj.buadRate = obj.buadRate;
                itemObj.parity = obj.parity;
                itemObj.dataBit = obj.dataBit;
                itemObj.stopBit = obj.stopBit;
                entities.SaveChanges();
                return new Response { IsSuccess = true, Message = "COM Port Configured successfully" };
            }
            else
                return new Response { IsSuccess = false, Message = "Configuration Error. Contact Administrator" };

        }
#endregion
    }
}