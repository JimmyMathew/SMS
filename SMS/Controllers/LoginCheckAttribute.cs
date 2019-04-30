using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS.Controllers
{
   
    public class LoginCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                var role = filterContext.HttpContext.Session["role"];
                if (role == null)
                    filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}