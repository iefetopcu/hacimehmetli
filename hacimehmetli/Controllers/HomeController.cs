using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hacimehmetli.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("MySessionUser");
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index");
        }

        

    }
}