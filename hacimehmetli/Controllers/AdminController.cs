using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hacimehmetli.Models.Entity;
using OfficeOpenXml;

namespace hacimehmetli.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        hacimehmetliEntities db = new hacimehmetliEntities();
        public ActionResult Index()
        {
            ViewBag.Kategori = (from s in db.categorytables                                 
                                  select s).Count();

            ViewBag.StoktaOlanUrunler = (from s in db.producttables
                                  where s.isitinstock == "true"
                                  select s).Count();
            ViewBag.StoktaOlmayanUrunler = (from s in db.producttables
                                         where s.isitinstock != "true"
                                         select s).Count();
            ViewBag.TumUrunler = (from s in db.producttables                                       
                                         select s).Count();

            ViewBag.IslemBekleyenSiparisler = (from s in db.ordertables
                                         where s.orderstatu.id == 1
                                         select s).Count();
            ViewBag.HazirlananSiparisler = (from s in db.ordertables
                                               where s.orderstatu.id == 2
                                               select s).Count();
            ViewBag.TeslimAsamasindaSiparisler = (from s in db.ordertables
                                               where s.orderstatu.id == 3
                                               select s).Count();
            ViewBag.TeslimEdilmisSiparisler = (from s in db.ordertables
                                               where s.orderstatu.id == 4
                                               select s).Count();
            ViewBag.ToplamSiparis = (from s in db.ordertables                                             
                                               select s).Count();

            ViewBag.OkunmamisMesajlar = (from s in db.contactforms
                                               where s.isaktif == "true"
                                               select s).Count();
            ViewBag.OkunmusMesajlar = (from s in db.contactforms
                                         where s.isaktif != "true"
                                         select s).Count();
            ViewBag.TumMesajlar = (from s in db.contactforms                                      
                                         select s).Count();

            return View();
        }
        public ActionResult Kategori()
        {
            var kategoriler = (from s in db.categorytables                              
                               select s).ToList();

            return View(kategoriler);
        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.categorytables.Find(id);
            db.categorytables.Remove(kategori);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(string categoryname)
        {
            var ekle = new categorytable
            {
                categoryname = categoryname,                             
            };
            db.categorytables.Add(ekle);
            db.SaveChanges();
            return RedirectToAction("Kategori");
        }
        [HttpGet]
        public ActionResult KategoriDuzenle(int id)
        {                     
            var category = db.categorytables.Find(id);
            return View("KategoriDuzenle", category);
        }
        [HttpPost]
        public ActionResult KategoriDuzenle(categorytable p1)
        {
            var category = db.categorytables.Find(p1.id);
            category.categoryname = p1.categoryname;           
            db.SaveChanges();
            return RedirectToAction("Kategori");
        }
        public ActionResult Urunler()
        {
           
            var urunler = (from s in db.producttables
                               select s).ToList();
            return View(urunler);
        }
        public ActionResult UrunSil(int id)
        {
            var urun = db.producttables.Find(id);
            db.producttables.Remove(urun);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult UrunDuzenle(int id)
        {
            List<SelectListItem> category = (from i in db.categorytables.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.categoryname,
                                                 Value = i.id.ToString()
                                             }).ToList();
            ViewBag.cat = category;
            var urun = db.producttables.Find(id);
            return View("UrunDuzenle", urun);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UrunDuzenle(producttable p1)
        {
            var product = db.producttables.Find(p1.id);
            
                if (p1.productphotourl != null)
                {
                    var userphoto = Request.Files[0];
                    var fileInfo = new FileInfo(userphoto.FileName);
                    var pic = "Product_" + DateTime.Now.Ticks + fileInfo.Extension;
                    var filePath = "/Documents/Product/" + pic;
                    var tempFilePath = Server.MapPath("~\\Documents\\Product\\" + pic);
                    userphoto.SaveAs(tempFilePath);
                product.productphotourl = filePath;
                }
            product.productporsiyon = p1.productporsiyon;
            product.productname = p1.productname;
            product.productdescription = p1.productdescription;
            product.productprice = p1.productprice;
            product.categoryid = p1.categoryid;
            product.isitinstock = p1.isitinstock;
            product.topsale = p1.topsale;
            product.vitrin = p1.vitrin;
            db.SaveChanges();
            return RedirectToAction("Urunler");
        }
        public ActionResult Siparis()
        {
            var siparisler = (from s in db.ordertables
                               select s).ToList();

            return View(siparisler);
        }
        public ActionResult SiparisNext(int id)
        {
            var status = db.ordertables.Find(id);
            status.statusid += 1;
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());

        }
        public ActionResult SiparisBack(int id)
        {
            var status = db.ordertables.Find(id);
            status.statusid -= 1;
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());

        }
        public ActionResult SiparisSil(int id)
        {
            var status = db.ordertables.Find(id);
            db.ordertables.Remove(status);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        public ActionResult SiparisGor(int id)
        {
            var orders = db.ordertables.Find(id);
            return View("SiparisGor", orders);
        }
        public ActionResult Mesajlar()
        {
            var mesaj = (from s in db.contactforms
                              select s).ToList();
            return View(mesaj);
        }
        public ActionResult MesajSil(int id)
        {
            var mesaj = db.contactforms.Find(id);
            db.contactforms.Remove(mesaj);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
        public ActionResult MesajOku(int id)
        {
            var mesaj = db.contactforms.Find(id);
            return View("MesajOku", mesaj);
        }
        public ActionResult Okundu(int id)
        {
            var mesaj = db.contactforms.Find(id);
            mesaj.isaktif = "null";
            db.SaveChanges();
            return RedirectToAction("Mesajlar");
        }
        public ActionResult Okunmadi(int id)
        {
            var mesaj = db.contactforms.Find(id);
            mesaj.isaktif = "true";
            db.SaveChanges();
            return RedirectToAction("Mesajlar");
        }
        public void ExcelExport(int id)
        {
            var siparis = db.ordertables.Find(id);

            var toplam = siparis.piece * siparis.producttable.productprice;
            var kdv = toplam / 100 * 18;
            var tutar = toplam - kdv;

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["F1:F30"].Style.Font.Size = 15;
            ws.Cells["B1:B30"].Style.Font.Size = 15;
            ws.Cells["A1:A30"].Style.Font.Size = 15;
            ws.Cells["A1:A30"].Style.Font.Bold = true;
            ws.Cells["B1:B30"].Style.Font.Bold = true;
            ws.Cells["F1:F30"].Style.Font.Bold = true;
            ws.Cells["G5:G10"].Style.Font.Size = 15;
            ws.Cells["G5:G10"].Style.Font.Bold = true;
            ws.Cells["A1"].Value = "SİPARİŞ BİLGİLERİ";
            
            ws.Cells["B1"].Value = "Ürün Adı";
            ws.Cells["C1"].Value = siparis.producttable.productname;
            ws.Cells["B2"].Value = "Adet";
            ws.Cells["C2"].Value = siparis.piece +"Adet/KG";
            ws.Cells["B3"].Value = "Teslimat Zamanı";
            ws.Cells["C3"].Value = siparis.deliverydate.ToString();

            
            ws.Cells["E1"].Value = "MÜŞTERİ BİLGİLERİ";
            ws.Cells["F1"].Value = "Müşteri Adı";
            ws.Cells["G1"].Value = siparis.namesurname;
            ws.Cells["F2"].Value = "Telefon Numarası";
            ws.Cells["G2"].Value = siparis.gsm;
            ws.Cells["F3"].Value = "Adres";
            ws.Cells["G3"].Value = siparis.adress;

            
            ws.Cells["F6"].Value = "TUTAR";
            ws.Cells["F7"].Value = "KDV";
            ws.Cells["F8"].Value = "TOPLAM";

            ws.Cells["G6"].Value = tutar +"₺";
            ws.Cells["G7"].Value = kdv + "₺"; ;
            ws.Cells["G8"].Value = toplam + "₺"; ;

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=" + siparis.namesurname +"_Siparisi" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
        public ActionResult QrMenu()
        {
            var qrmenu = (from s in db.menus
                           select s).ToList();
            return View(qrmenu);
        }
        public ActionResult QrMenuSil(int id)
        {
            var urun = db.producttables.Find(id);
            db.producttables.Remove(urun);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
    }
}