using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hacimehmetli.Models.Entity;

namespace hacimehmetli.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (usertable)filterContext.HttpContext.Session["MySessionUser"];
            if (session != null)
            {
                base.OnActionExecuting(filterContext);
                return;

            }
            else
            {
                filterContext.Result = new RedirectResult("/");
                base.OnActionExecuting(filterContext);
            }

            filterContext.Result = new RedirectResult("/");
            base.OnActionExecuting(filterContext);
        }
    }
}