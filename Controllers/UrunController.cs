using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SekerWeb.Models.Entity;
using System.IO;

namespace SekerWeb.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        DiyabetimEntities db = new DiyabetimEntities();
        public ActionResult Index()
        {
            var urun = db.Urun.ToList();
            return View(urun);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {

            List<SelectListItem> degerler = (from i in db.Kategori.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Kategoriler,
                                                 Value = i.id.ToString()
                                             }).ToList();
            ViewBag.dgr1 = degerler;

            List<SelectListItem> degerler1 = (from k in db.Marka.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = k.MarkaAdı,
                                                  Value = k.id.ToString()
                                              }).ToList();
            ViewBag.dgr2 = degerler1;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun p)
        {
            var ktgr = db.Kategori.Where(m => m.id == p.İd).FirstOrDefault();
            var marka = db.Marka.Where(m => m.id == p.İd).FirstOrDefault();
            p.Kategori = ktgr;
            p.Marka = marka;

            //string dosyaadi = Path.GetFileName(file.FileName);
            //string _dosyaadi = DateTime.Now.ToString("yymmssfff") + dosyaadi;
            //string ex = Path.GetExtension(file.FileName);
            //string path = Path.Combine(Server.MapPath("~/images/"), _dosyaadi);
            //p.Resim = "~/images/" + _dosyaadi;

            if (Request.Files.Count > 0)
            {
                var dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                var uzanti = Path.GetExtension(Request.Files[0].FileName);
                var yol = "~/images/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.Resim = "~/images/" + dosyaadi + uzanti;
            }

                //}
                //if (ex.ToLower() == ".jpg" || ex.ToLower() == ".jpeg" || ex.ToLower() == ".png")
                //{
                //    if (file.ContentLength <= 1000000)
                //    {
                //        db.Urun.Add(p);
                //        if (db.SaveChanges() > 0)
                //        {
                //            file.SaveAs(path);
                //            ViewBag.msg = "Eklendi";
                //            ModelState.Clear();
                //        }
                //    }
                //    else
                //    {
                //        ViewBag.msg = "Eklenmedi";
                //    }

                //}

                db.Urun.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }
        public ActionResult UrunSil(int id)
        {
            
            var hasta = db.Urun.Find(id);
            db.Urun.Remove(hasta);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
      
    }
}