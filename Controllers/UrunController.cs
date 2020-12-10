using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SekerWeb.Models.Entity;

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
        public ActionResult UrunSil(int id)
        {
            var hasta = db.Urun.Find(id);
            db.Urun.Remove(hasta);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}