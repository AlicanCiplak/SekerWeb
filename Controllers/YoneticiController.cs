using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SekerWeb.Models.Entity;
using SekerWeb.Sınıflar;

namespace SekerWeb.Controllers
{
    public class YoneticiController : Controller
    {
        // GET: Yonetici
        DiyabetimEntities db = new DiyabetimEntities();
        Crud ab = new Crud();


        public ActionResult Index()
        {

            var hasta = db.Hasta.ToList();
            return View(hasta);
        }
        [HttpGet]
        public ActionResult HastaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult HastaEkle(Hasta hasta)
        {
            db.Hasta.Add(hasta);
            db.SaveChanges();
            return View();
        }

        public ActionResult HastaSil(int id)
        {
            var hasta = db.Hasta.Find(id);
            db.Hasta.Remove(hasta);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult YoneticiGiris(Yönetici p)
        {
            var yonetici = db.Yönetici.FirstOrDefault(x => x.KullanıcıAdı == p.KullanıcıAdı && x.Sifre == p.Sifre);

            if (yonetici != null)
            {
                FormsAuthentication.SetAuthCookie(yonetici.KullanıcıAdı, false);
                Session["YonericiİD"] = yonetici.id.ToString();
                Session["YoneticiAdSoyad"] = yonetici.AdSoyad.ToString();
                Session["KullancıAdı"] = yonetici.KullanıcıAdı.ToString();
                Session["YoneticiSifre"] = yonetici.Sifre.ToString();

                return RedirectToAction("YoneticiProfil", "Yonetici");
            }
            else if (yonetici == null)
            {
                ViewBag.msj = "Kullanıcı adı veya şifre geçersiz";
                return View();
            }
            else
            {
                return View();
            }
        }
        public ActionResult YoneticiProfil()
        {
            return View();
        }

        public ActionResult DuyuruListele()
        {
            var duyuru = db.Duyuru.ToList();
            return View(duyuru);
        }
        [HttpGet]
        public ActionResult DuyuruEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DuyuruEkle(Duyuru duyuru)
        {
            duyuru.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.Duyuru.Add(duyuru);
            db.SaveChanges();
            return View();
        }
        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.Duyuru.Find(id);
            db.Duyuru.Remove(duyuru);
            db.SaveChanges();
            return RedirectToAction("DuyuruListele");

        }

        public ActionResult DuyuruGetir(int id)
        {
            var duy = db.Duyuru.Find(id);
            return View("DuyuruGetir", duy);
        }
        public ActionResult DuyuruGüncelle(Duyuru duy)
        {
            var duyuru = db.Duyuru.Find(duy.İD);
            duyuru.Konu = duy.Konu;
            duyuru.Duyurular = duy.Duyurular;
            duyuru.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString()); 
            db.SaveChanges();
            return RedirectToAction("DuyuruListele");
        }

        public ActionResult DoktorListele()
        {
            var hekim = db.Hekim.ToList();
            return View(hekim);
        }
        [HttpGet]
        public ActionResult DoktorEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoktorEkle(Hekim hekim)
        {

            db.Hekim.Add(hekim);
            db.SaveChanges();
            return View();
        }
        public ActionResult DoktorSil(int id)
        {
            var hekim = db.Hekim.Find(id);
            db.Hekim.Remove(hekim);
            db.SaveChanges();
            return RedirectToAction("DoktorListele");
        }
        public ActionResult AtamaListele()
        {
            var atama = db.DoktorHasta.ToList();
            return View(atama);

        }
        [HttpGet]
        public ActionResult DoktorAtama()
        {
            List<SelectListItem> degerler = (from k in db.Hasta.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = k.AdSoyad,
                                                 Value = k.İD.ToString()
                                             }).ToList();
            List<SelectListItem> degerler1 = (from k in db.Hekim.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = k.AdSoyad,
                                                  Value = k.İd.ToString()
                                              }).ToList();


            ViewBag.dgr = degerler;
            ViewBag.dgr1 = degerler1;

            return View();

        }
        [HttpPost]
        public ActionResult DoktorAtama(DoktorHasta atama)
        {
            var hasta = db.Hasta.Where(m => m.İD == atama.HastaİD).FirstOrDefault();
            var doktor = db.Hekim.Where(m => m.İd == atama.DoktorİD).FirstOrDefault();
        

            atama.Hasta = hasta;
            atama.Hekim = doktor;
            atama.Tarih= DateTime.Parse(DateTime.Now.ToShortDateString());


            db.DoktorHasta.Add(atama);
            db.SaveChanges();
            return RedirectToAction("AtamaListele", "Yonetici");
            
        }
    }
    }