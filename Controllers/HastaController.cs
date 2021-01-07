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
        

    }
}