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
        [HttpGet]
        public ActionResult SiparisVer(int? id)
        {                                  
                List<SelectListItem> urun = (from i in db.producttables.ToList()
                                             select new SelectListItem
                                             {                                                 
                                                 Selected = (i.id == id),
                                                 Text = i.productname,
                                                 Value = i.id.ToString()
                                             }).ToList();
            ViewBag.urun = urun;                           
            ViewBag.success = TempData["success"];
            return View("SiparisVer");
        }
        [HttpPost]
        public ActionResult SiparisVer(int id, int piece, string namesurname, string gsm, string description, string adress, string deliverydate)
        {
            var zaman = DateTime.Now.ToString();
            string sipariszamani = deliverydate;
            var ekle = new ordertable
            {
                productid = id,
                statusid = 1,
                piece = piece,
                description = description,
                adress = adress,
                gsm = gsm,
                namesurname = namesurname,
                deliverydate = Convert.ToDateTime(sipariszamani),
                creationdate = Convert.ToDateTime(zaman),
            };
            var urunadi = db.producttables.Find(id).productname;
            SmtpClient client = new SmtpClient("mail.hacimehmetetli.com", 587);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("info@hacimehmetetli.com", "Yeni Sipariş");
            mail.To.Add("hacimehmetetlibaklava@gmail.com");
            mail.IsBodyHtml = true;
            mail.Subject = "Yeni Sipariş " + DateTime.Now.ToString();
            mail.Body += "Yeni Sipariş Geldi ! <br/><br/> "
                + "<b>Sipariş Veren : </b> " + namesurname + "<br/>"
                + "<b>Sipariş Tarihi :</b> " + zaman + "<br/>"
                + "<b>Sipariş Bilgileri</b> <br/>"
                + "<b>Ürün :</b> " + urunadi + "<br/>"
                + "<b>Adet :</b> " + piece + "<br/>"
                + "<b>Teslimat Zamanı :</b> " + deliverydate + "<br/>"
                + "<b>Telefon :</b> " + gsm + "<br/>"
                + "<b>Adres :</b> " + adress + "<br/>"
                + "<b>Açıklama :</b> " + description + "<br/>"
                + "<br/><br/>"
                + "Siparişi hacimehmetetli.com adresinden görüntüleyebilir, çıktı alabilirsiniz... <br/>"
                + "<a href='https://hacimehmetetli.com/Panel'>Panele Git</a>"
            ;

            NetworkCredential net = new NetworkCredential("info@hacimehmetetli.com", "$non524B");
            client.Credentials = net;
            client.Send(mail);

            db.ordertables.Add(ekle);
            db.SaveChanges();
            TempData["success"] = string.Format("ok");
            return Redirect(Request.UrlReferrer.ToString());
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