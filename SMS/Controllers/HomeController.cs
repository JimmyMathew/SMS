using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMS.Models;
using SMS.DAL;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        MasterDAL masterDal = new MasterDAL();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginCheck()
        {
            string username = Request["username"].ToString();
            string password = Request["password"].ToString();

            var obj = masterDal.ReadUserDetails().Where(a => a.username.Equals(username) && a.password.Equals(password)).FirstOrDefault();
            if (obj != null)
            {
                Session["employeeName"] = obj.employeeName.ToString();
                Session["role"] = obj.role.ToString();
                Session["username"] = obj.username.ToString();
                //return View(new { redirecturl = "/Home/Index" }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index", "Weighment");
            }
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "Home");
        }
        [LoginCheckAttribute]
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
               // WeighmentController w = new WeighmentController();
               //w.SetSerialPortConfig();
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }
        [LoginCheckAttribute]
        public ActionResult Com()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReadComDetails()
        {
            List<ComDetails> comDetails = new List<ComDetails>();
            comDetails = masterDal.ReadComDetails().ToList();
            return Json(comDetails, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ConfigureComPort(string id, string port, string buadRate, string parity, string dataBit, string stopBit)
        {

            Response res = new Response();
            List<object> resultList = new List<object>();
            ComDetails item = new ComDetails();
            item.port = port;
            item.id = long.Parse(id);
            item.buadRate = buadRate;
            item.parity = parity;
            item.dataBit = dataBit;
            item.stopBit = stopBit;
            
            res = masterDal.UpdateComDetails(item);
            resultList.Add(res);
            resultList.Add(ReadComDetails());


            return Json(resultList, JsonRequestBehavior.AllowGet);
        }

    }
}