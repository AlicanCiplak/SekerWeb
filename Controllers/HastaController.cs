using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SekerWeb.Models.Entity;
namespace SekerWeb.Controllers
{
    public class HastaController : Controller
    {
        // GET: Hasta
        DiyabetimEntities db = new DiyabetimEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HastaProfil()
        {
            return View();
        }

        public ActionResult HastaGiris(Hasta hasta)
        {
            var has = db.Hasta.FirstOrDefault(x => x.KullanıcıAdı == hasta.KullanıcıAdı && x.Sifre == hasta.Sifre);

            if (has != null)
            {
                FormsAuthentication.SetAuthCookie(hasta.KullanıcıAdı, false);
                Session["HastaİD"] = has.İD;
                Session["HastaAdSoyad"] = has.AdSoyad.ToString();
                Session["KullancıAdı"] = has.KullanıcıAdı.ToString();
                Session["Sifre"] = has.Sifre.ToString();
                Session["TEL"] = has.TelefonNumarası.ToString();
                Session["TC"] = has.TC.ToString();
                //Session["Foto"] = hek.Foto.ToString();

                return RedirectToAction("HastaProfil", "Hasta");
            }
            else if (has == null)
            {
                ViewBag.msj = "Kullanıcı adı veya şifre geçersiz";
                return View();
            }
            else
            {
                return View();
            }

        }
        public ActionResult SekerListeleme()
        {
            var sekerlistele = (int)Session["HastaİD"];
            var liste = db.Sekerlerim.Where(x => x.HastaİD == sekerlistele).ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult SekerEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SekerEkle(Sekerlerim seker)
        {
            seker.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            seker.HastaİD= (int)Session["HastaİD"];
            db.Sekerlerim.Add(seker);
            db.SaveChanges();
            return View();
        }
        public ActionResult BesinListele()
        {
            var besinlistele= (int)Session["HastaİD"];
            var liste = db.Besin.Where(x => x.HastaİD == besinlistele).ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult BesinEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BesinEkle(Besin besin)
        {
            besin.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            besin.HastaİD = (int)Session["HastaİD"];
            db.Besin.Add(besin);
            db.SaveChanges();
            return View();
        }

        public ActionResult TEST()
        {
            var testlistele = (int)Session["HastaİD"];
            var test = db.TestAtama.Where(x => x.HastaİD == testlistele).ToList();
            return View(test);
        }

        public ActionResult UrunListe()
        {
            var urun = db.Urun.ToList();
            return View(urun);
        }
        public ActionResult SepeteEkle(int id)
        {
            var urun = db.Urun.Find(id);
            var dene = db.Sepetim.Count(x => x.UrunİD == id);
            if (dene == 0)
            {
                Sepetim sepet = new Sepetim();
                sepet.HastaİD = (int)Session["HastaİD"];
                sepet.UrunİD = id;
                sepet.Adet = 1;
                sepet.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            
                //db.Sepetim.Add(sepet);
                //db.SaveChanges();


                sepet.ToplamFiyat = sepet.Adet * urun.Fiyat; 
                db.Sepetim.Add(sepet);
                db.SaveChanges();
                //var tutar = db.Sepetim.Where(x => x.İD == sepet.İD).Sum(x => x.Urun.Fiyat);
                //tutar = tutar * sepet.Adet;
                //ViewBag.TOPLAM = tutar;
                return RedirectToAction("UrunListe");
            }
            else
            {
                return RedirectToAction("UrunListe");
            }
        }
        public ActionResult Sepetim()
        {
            var hastaİD = (int)Session["HastaİD"];
            var tutar = db.Sepetim.Where(x => x.HastaİD == hastaİD).Sum(x => x.ToplamFiyat);
            ViewBag.tutar = tutar;
            var sepet = db.Sepetim.Where(x => x.HastaİD == hastaİD).ToList();

            return View(sepet);
        }

        public ActionResult Arti(int id)
        {
            var arti = db.Sepetim.Find(id);
            arti.Adet++;
            arti.ToplamFiyat = arti.Adet * arti.Urun.Fiyat;
            db.SaveChanges();
            //Session["ToplamFiyat"] =arti.Adet*arti.Urun.Fiyat ;
            return RedirectToAction("Sepetim");
        }
        public ActionResult Azalt(int id)
        {
            var arti = db.Sepetim.Find(id);
            if (arti.Adet == 0 )
            {
                db.Sepetim.Remove(arti);
                db.SaveChanges();
                return RedirectToAction("sepetim");
            }
            else
            {
                arti.Adet--;
                arti.ToplamFiyat = arti.Adet * arti.Urun.Fiyat;
                db.SaveChanges();
                //Session["ToplamFiyat1"] = arti.Adet * arti.Urun.Fiyat;
                return RedirectToAction("Sepetim");
            }
        }
        public ActionResult SepetSil(int id)
        {
            var sepet = db.Sepetim.Find(id);
            db.Sepetim.Remove(sepet);
            db.SaveChanges();
            return RedirectToAction("Sepetim");
        }
      public ActionResult UrunDetay(int id)
        {
            var urun = db.Urun.Where(x=>x.İd==id).ToList();
            return View(urun);
        }
    }
}