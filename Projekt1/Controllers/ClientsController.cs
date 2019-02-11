using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projekt1.Controllers
{
    public class ClientsController : Controller
    {
        // GET: Clients
        public ActionResult Index()
        {
            var entities = new Models.Database1Entities();
            return View(entities.Clients.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Clients client)
        {
            var db = new Models.Database1Entities();

            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(client);
        }

        public ActionResult Delete(int? id)
        {
            var db = new Models.Database1Entities();
            Models.Clients client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }


        [HttpPost]

        public ActionResult Delete(int id, FormCollection collection)
        {
            var db = new Models.Database1Entities();
            Models.Clients client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
            Models.Clients client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,surname,adress")] Models.Clients client)
        {
            var db = new Models.Database1Entities();

            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }
    }
}