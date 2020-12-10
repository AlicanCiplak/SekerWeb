using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SekerWeb.Models.Entity;

namespace SekerWeb.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DiyabetimEntities db = new DiyabetimEntities();
        public ActionResult Index()
        {
            var kat = db.Kategori.ToList();
            return View(kat);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori p)
        {
            db.Kategori.Add(p);
            db.SaveChanges();
            return View();
        }
        public ActionResult KategoriGetir(Kategori kat)
        {
            var kategori = db.Kategori.Find(kat.id);
            return View("KategoriGetir", kategori);
        }
        public ActionResult KategoriGüncelle(Kategori kat)
        {
            var kategori = db.Kategori.Find(kat.id);
            kategori.Kategoriler = kat.Kategoriler;
           
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}