using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SekerWeb.Models.Entity;
namespace SekerWeb.Sınıflar
{
    public class Crud
    {
        DiyabetimEntities db = new DiyabetimEntities();   
        
       
        public void HastaEkleme(Hasta hasta)
        {
            db.Hasta.Add(hasta);
            db.SaveChanges();
            
            
        }
        public void HastaSil(int i)
        {
            var hasta = db.Hasta.Find(i);
            db.Hasta.Remove(hasta);
            db.SaveChanges();
        }
    }
}