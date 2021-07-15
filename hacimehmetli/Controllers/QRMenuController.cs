using hacimehmetli.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hacimehmetli.Controllers
{
    public class QRMenuController : Controller
    {
        hacimehmetliEntities db = new hacimehmetliEntities();
        // GET: QRMenu
        public ActionResult Index()
        {
            var qrmenu = (from s in db.menus
                          where s.stokdurumu == "true"
                          select s).ToList();

            return View(qrmenu);
        }
    }
}