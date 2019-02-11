using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projekt1.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index()
        {
            var entities = new Models.Database1Entities();
            return View(entities.Brand.ToList());

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Brand brand)
        {
            var db = new Models.Database1Entities();

            if (ModelState.IsValid)
            {
                db.Brand.Add(brand);
                db.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(brand);
        }

        public ActionResult Delete(int? id)
        {
            var db = new Models.Database1Entities();
            Models.Brand brand = db.Brand.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }


        [HttpPost]
        
        public ActionResult Delete(int id, FormCollection collection)
        {
            var db = new Models.Database1Entities();
            Models.Brand brand = db.Brand.Find(id);
            db.Brand.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            var db = new Models.Database1Entities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Brand magazine = db.Brand.Find(id);
            if (magazine == null)
            {
                return HttpNotFound();
            }
            return View(magazine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_brand,name")] Models.Brand brand)
        {
            var db = new Models.Database1Entities();

            if (ModelState.IsValid)
            {
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }
    }
}