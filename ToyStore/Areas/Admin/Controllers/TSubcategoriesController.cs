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
    public class TSubcategoriesController : Controller
    {
        private ToyStoreContext db = new ToyStoreContext();

        // GET: Admin/TSubcategories
        public ActionResult Index()
        {
            return View(db.TSubcategories.ToList());
        }

        // GET: Admin/TSubcategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TSubcategory tSubcategory = db.TSubcategories.Find(id);
            if (tSubcategory == null)
            {
                return HttpNotFound();
            }
            return View(tSubcategory);
        }

        // GET: Admin/TSubcategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TSubcategories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idSubcategory,Subcategory")] TSubcategory tSubcategory)
        {
            if (ModelState.IsValid)
            {
                db.TSubcategories.Add(tSubcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tSubcategory);
        }

        // GET: Admin/TSubcategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TSubcategory tSubcategory = db.TSubcategories.Find(id);
            if (tSubcategory == null)
            {
                return HttpNotFound();
            }
            return View(tSubcategory);
        }

        // POST: Admin/TSubcategories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idSubcategory,Subcategory")] TSubcategory tSubcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tSubcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tSubcategory);
        }

        // GET: Admin/TSubcategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TSubcategory tSubcategory = db.TSubcategories.Find(id);
            if (tSubcategory == null)
            {
                return HttpNotFound();
            }
            return View(tSubcategory);
        }

        // POST: Admin/TSubcategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TSubcategory tSubcategory = db.TSubcategories.Find(id);
            db.TSubcategories.Remove(tSubcategory);
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
