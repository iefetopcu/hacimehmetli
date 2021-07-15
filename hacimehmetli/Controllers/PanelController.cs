using hacimehmetli.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hacimehmetli.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
        hacimehmetliEntities db = new hacimehmetliEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(usertable model)
        {
            var hacimehmetetliUser = db.usertables.FirstOrDefault(x => x.email == model.email && x.password == model.password);
            if(hacimehmetetliUser != null)
            {
                Session["MySessionUser"] = hacimehmetetliUser;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}