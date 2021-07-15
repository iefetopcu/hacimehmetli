using hacimehmetli.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hacimehmetli.Controllers
{
    public class PartialAdminController : BaseController
    {
        // GET: PartialAdmin
        hacimehmetliEntities db = new hacimehmetliEntities();
        [HttpGet]
        public ActionResult UrunForm()
        {
            List<SelectListItem> category = (from i in db.categorytables.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.categoryname,
                                                 Value = i.id.ToString()

                                             }).ToList();           
            ViewBag.cat = category;
            return PartialView();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UrunEkle(string productporsiyon, string productname, string productdescription, string productphotourl, int productprice, int categoryid, string vitrin,string topsale , string isinstock)
        {
            
            var ekle = new producttable
            {
                productname = productname,
                productdescription = productdescription,
                categoryid = categoryid,
                productprice = productprice,
                isaktif = 1,
                isitinstock = isinstock,
                vitrin = vitrin,
                topsale = topsale,
                productporsiyon = productporsiyon,
                
            };
            if (productphotourl != null)
            {
                var userphoto = Request.Files[0];
                var fileInfo = new FileInfo(userphoto.FileName);
                var pic = "Product_" + DateTime.Now.Ticks + fileInfo.Extension;
                var filePath = "/Documents/Product/" + pic;
                var tempFilePath = Server.MapPath("~\\Documents\\Product\\" + pic);
                userphoto.SaveAs(tempFilePath);
                ekle.productphotourl = filePath;
            }
            db.producttables.Add(ekle);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult QRMenu()
        {
            List<SelectListItem> category = (from i in db.categorytables.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.categoryname,
                                                 Value = i.id.ToString()

                                             }).ToList();
            ViewBag.cat = category;
            return PartialView();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult QRMenuEkle(string urunadi, string urunaciklamasi, string urunphotourl, int urunfiyati, int categoryid, string stokdurumu, string urunporsiyon)
        {
            var ekle = new menu
            {
                urunadi = urunadi,
                urunaciklamasi = urunaciklamasi,
                stokdurumu = stokdurumu,
                urunfiyati = urunfiyati,
                categoryid = categoryid,
                urunporsiyon = urunporsiyon,        
            };
            if (urunphotourl != null)
            {
                var userphoto = Request.Files[0];
                var fileInfo = new FileInfo(userphoto.FileName);
                var pic = "QrMenu_" + DateTime.Now.Ticks + fileInfo.Extension;
                var filePath = "/Documents/QrMenu/" + pic;
                var tempFilePath = Server.MapPath("~\\Documents\\QrMenu\\" + pic);
                userphoto.SaveAs(tempFilePath);
                ekle.urunphotourl = filePath;
            }
            db.menus.Add(ekle);
            db.SaveChanges();
            return Redirect(ControllerContext.HttpContext.Request.UrlReferrer.ToString());
        }
    }
}