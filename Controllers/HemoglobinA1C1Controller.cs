using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SekerWeb.Models.Entity;

namespace SekerWeb.Controllers
{
    public class HemoglobinA1C1Controller : Controller
    {
        DiyabetimEntities db = new DiyabetimEntities();

        // GET: HemoglobinA1C1
        public ActionResult Index()
        {
            db.Sekerlerim.ToList();
            return View();
        }

        public ActionResult HemoglobinTahmin(Sekerlerim s,int id)
        {
            var oglenAc = db.Sekerlerim.Where(x => x.İD == id).Sum(x => x.OglenAclik);
            var sabahAc = db.Sekerlerim.Where(x => x.İD == id).Sum(x => x.SabahAclik);
            var aksamAc = db.Sekerlerim.Where(x => x.İD == id).Sum(x => x.AksamAclık);
            var sabahTok = db.Sekerlerim.Where(x => x.İD == id).Sum(x => x.SabahTokluk);
            var oglentok = db.Sekerlerim.Where(x => x.İD == id).Sum(x => x.OglenTokluk);
            var aksamTok = db.Sekerlerim.Where(x => x.İD == id).Sum(x => x.AksamTokluk);
            var yatmadanönce = db.Sekerlerim.Where(x => x.İD == id).Sum(x => x.Gece);

            var hemo = (aksamAc + sabahAc + oglenAc + sabahTok + oglentok + aksamTok + yatmadanönce) / 30;

            return View();
        }

        public ActionResult Hastalar()
        {
            var hastalarlistele = (int)Session["HekimİD"];
            var liste = db.DoktorHasta.Where(x => x.DoktorİD == hastalarlistele).ToList();
            return View(liste);
        }
        public ActionResult SekerDegerleri(int id)
        {
            var seker = db.Sekerlerim.Where(x => x.HastaİD == id).OrderByDescending(x => x.İD).ToList();
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

    }
}