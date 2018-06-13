using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToyStore.Models;

namespace ToyStore.Areas.Admin.Controllers
{
    public class TCategoriesController : Controller
    {
        private ToyStoreContext db = new ToyStoreContext();

        // GET: Admin/TCategories
        public ActionResult Index()
        {
            return View(db.TCategories.ToList());
        }


        // GET: Admin/TCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TCategories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCategorie,Category")] TCategorie tCategorie)
        {
            if (ModelState.IsValid)
            {
                db.TCategories.Add(tCategorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tCategorie);
        }

        // GET: Admin/TCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TCategorie tCategorie = db.TCategories.Find(id);
            if (tCategorie == null)
            {
                return HttpNotFound();
            }
            return View(tCategorie);
        }

        // POST: Admin/TCategories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCategorie,Category")] TCategorie tCategorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tCategorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tCategorie);
        }

        // GET: Admin/TCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TCategorie tCategorie = db.TCategories.Find(id);
            if (tCategorie == null)
            {
                return HttpNotFound();
            }
            return View(tCategorie);
        }

        // POST: Admin/TCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TCategorie tCategorie = db.TCategories.Find(id);
            db.TCategories.Remove(tCategorie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
