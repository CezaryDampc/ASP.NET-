using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projekt1.Controllers
{
    public class ModelController : Controller
    {
        // GET: Model
        public ActionResult Index()
        {
            var entities = new Models.Database1Entities();
            return View(entities.Model.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Model model)
        {
            var db = new Models.Database1Entities();

            if (ModelState.IsValid)
            {
                db.Model.Add(model);
                db.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        public ActionResult Delete(int? id)
        {
            var db = new Models.Database1Entities();
            Models.Model model = db.Model.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }


        [HttpPost]

        public ActionResult Delete(int id, FormCollection collection)
        {
            var db = new Models.Database1Entities();
            Models.Model model = db.Model.Find(id);
            db.Model.Remove(model);
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
            Models.Model model = db.Model.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Price,Course")] Models.Model model)
        {
            var db = new Models.Database1Entities();

            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}