using hacimehmetli.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hacimehmetli.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        hacimehmetliEntities db = new hacimehmetliEntities();
        public ActionResult Kategoriler()
        {
            var degerler = (from s in db.categorytables                           
                           select s
                            ).ToList();
            return PartialView(degerler);
        }
        public ActionResult FooterKategoriler()
        {
            var degerler = (from s in db.categorytables
                            select s
                            ).ToList();
            return PartialView(degerler);
        }
        public ActionResult Vitrin()
        {
            var vitrin = db.producttables.Where(x => x.vitrin == "true" && x.isitinstock == "true" ).OrderBy(x => Guid.NewGuid()).Take(12);
            return PartialView(vitrin);
        }
        public ActionResult EnCokSatanlar()
        {
            var encoksatanlar = db.producttables.Where(x => x.topsale == "true" && x.isitinstock == "true").OrderBy(x => Guid.NewGuid()).Take(12);
            return PartialView(encoksatanlar);
        }

        public ActionResult Menu1()
        {
            var qrmenu = (from s in db.menus
                          where s.categoryid == 1 && s.stokdurumu == "true"
                          select s).ToList();
            var catname = from x in db.categorytables
                         where x.id == 1
                         select x.categoryname;
            ViewBag.CategoryName = catname;
            return View(qrmenu);
        }

        public ActionResult Slider()
        {
            return PartialView();
        }
    }
}