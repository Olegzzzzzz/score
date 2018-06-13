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
    public class TBuyersController : Controller
    {
        private ToyStoreContext db = new ToyStoreContext();

        // GET: Admin/TBuyers
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var tBuyers = db.TBuyers.Include(t => t.TRole);
            return View(tBuyers.ToList());
        }

       

        // GET: Admin/TBuyers/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.C_TRole = new SelectList(db.TRoles, "IdRole", "Name");
            return View();
        }

        // POST: Admin/TBuyers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create([Bind(Include = "idBuyer,Firstname,Email,Password,C_TRole")] TBuyer tBuyer)
        {
            if (ModelState.IsValid)
            {
                db.TBuyers.Add(tBuyer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_TRole = new SelectList(db.TRoles, "IdRole", "Name", tBuyer.C_TRole);
            return View(tBuyer);
        }

        // GET: Admin/TBuyers/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBuyer tBuyer = db.TBuyers.Find(id);
            if (tBuyer == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_TRole = new SelectList(db.TRoles, "IdRole", "Name", tBuyer.C_TRole);
            return View(tBuyer);
        }

        // POST: Admin/TBuyers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "idBuyer,Firstname,Email,Password,C_TRole")] TBuyer tBuyer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tBuyer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_TRole = new SelectList(db.TRoles, "IdRole", "Name", tBuyer.C_TRole);
            return View(tBuyer);
        }

        // GET: Admin/TBuyers/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TBuyer tBuyer = db.TBuyers.Find(id);
            if (tBuyer == null)
            {
                return HttpNotFound();
            }
            return View(tBuyer);
        }

        // POST: Admin/TBuyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            TBuyer tBuyer = db.TBuyers.Find(id);
            db.TBuyers.Remove(tBuyer);
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
