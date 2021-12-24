using hacimehmetli.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace hacimehmetli.Controllers
{
    public class UrunlerController : Controller
    {
        hacimehmetliEntities db = new hacimehmetliEntities();
        // GET: Urunler
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Kategori(int id)
        {
            var values = (from s in db.producttables
                          where s.categoryid == id && s.isaktif == 1 && s.isitinstock == "true"                         
                          select s
                           ).ToList();

            var catname = db.categorytables.Where(x => x.id == id).Select(x => x.categoryname).SingleOrDefault();

            ViewBag.Categoryname = catname.ToString();
            

            return View("Kategori", values);

        }
        

        
        public ActionResult Urun(int id)
        {
            var uruns = db.producttables.Find(id);
            return View("Urun", uruns);            
        }

        public ActionResult HomeSearch(string searchkey)
        {
            var search = from x in db.producttables
                         select x;
            if (!String.IsNullOrEmpty(searchkey))
            {
                search = search.Where(s => s.productname.Contains(searchkey)
                || s.productdescription.Contains(searchkey)
                );
            }
            ViewBag.Words = searchkey;
            return View(search.ToList());
        }
    }
}