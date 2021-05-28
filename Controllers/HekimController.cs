using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SekerWeb.Models.Entity;
using System.Web.Security;

namespace SekerWeb.Controllers
{
    public class HekimController : Controller
    {
        // GET: Hekim
        DiyabetimEntities db = new DiyabetimEntities();
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult HekimProfil()
        {
            return View();
        }

        public ActionResult HekimGiris(Hekim hekim)
        {
            var hek = db.Hekim.FirstOrDefault(x => x.KullanıcıAd == hekim.KullanıcıAd && x.Sifre == hekim.Sifre);

            if (hek != null)
            {
                FormsAuthentication.SetAuthCookie(hek.KullanıcıAd, false);
                Session["HekimİD"] = hek.İd;
                Session["HekimAdSoyad"] = hek.AdSoyad.ToString();
                Session["KullancıAdı"] = hek.KullanıcıAd.ToString();
                Session["Sifre"] = hek.Sifre.ToString();
                Session["Üniversite"] = hek.Universite.ToString();
                Session["Akademik"] = hek.AkademikHayat.ToString();
                //Session["Foto"] = hek.Foto.ToString();

                return RedirectToAction("HekimProfil", "Hekim");
            }
            else if (hek == null)
            {
                ViewBag.msj = "Kullanıcı adı veya şifre geçersiz";
                return View();
            }
            else
            {
                return View();
            }

        }
        public ActionResult Hastalar()
        {
            var hastalarlistele= (int)Session["HekimİD"];
            var liste = db.DoktorHasta.Where(x => x.DoktorİD ==hastalarlistele).ToList();
            return View(liste);
        }
        public ActionResult SekerDegerleri( int id)
        {
            var seker = db.Sekerlerim.Where(x => x.HastaİD == id).OrderByDescending(x=>x.İD).ToList();
            return View(seker);
        }
        public ActionResult Beslenme(int id)
        {
            var bes = db.Besin.Where(x => x.HastaİD == id).OrderByDescending(x => x.iD).ToList();
            return View(bes);
        }

        public ActionResult TestListele(int id)
        {
            var test = db.TestAtama.Where(x => x.HastaİD == id).OrderByDescending(x => x.İD).ToList();
            return View(test);
        }
        public ActionResult HastaİDAl(int id)
        {
            var hasta = db.Hasta.Find(id);
            Session["HastaiD"] = hasta.İD;
            return RedirectToAction("Mesaj");
        }
        [HttpGet]
        public ActionResult Mesaj()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Mesaj(Mesaj m)
        {
           var hekimİD=(int)Session["HekimİD"];
           var hastaİD= (int)Session["HastaiD"];
           
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.HastaİD = hastaİD;
            m.HekimİD = hekimİD;
            m.Gonderen = hekimİD;
            
            db.Mesaj.Add(m);
            db.SaveChanges();
            return View();
        }
    }
}