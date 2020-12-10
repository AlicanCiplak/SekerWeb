using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SekerWeb.Models.Entity;
namespace SekerWeb.Controllers
{
    public class MarkaController : Controller
    {
        // GET: Marka
        DiyabetimEntities db = new DiyabetimEntities();
        public ActionResult Index()
        {
            var marka = db.Marka.ToList();
            return View(marka);
        }

        [HttpGet]
        public ActionResult MarkaEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MarkaEkle(Marka p)
        {
            db.Marka.Add(p);
            db.SaveChanges();
            return View();
        }
    }
}