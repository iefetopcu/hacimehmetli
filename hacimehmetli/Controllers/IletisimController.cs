using hacimehmetli.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hacimehmetli.Controllers
{
    public class IletisimController : Controller
    {
        // GET: Iletisim
        hacimehmetliEntities db = new hacimehmetliEntities();
        public ActionResult Index()
        {
            ViewBag.success = TempData["success"];
            return View();
        }

        [HttpPost]
        public ActionResult Index(string namesurname, string title,string description, string gsm)
        {
            var zaman = DateTime.Now.ToString();
            var ekle = new contactform
            {
                namesurname = namesurname,
                title = title,
                description = description,
                gsm = gsm,
                isaktif = "true",
                messagetime = Convert.ToDateTime(zaman),
            };

            db.contactforms.Add(ekle);
            db.SaveChanges();
            TempData["success"] = string.Format("ok");
            return Redirect(Request.UrlReferrer.ToString());


        }
    }
}