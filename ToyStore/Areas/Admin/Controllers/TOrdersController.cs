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
    public class TOrdersController : Controller
    {
        private ToyStoreContext db = new ToyStoreContext();

        // GET: Admin/TOrders
        public ActionResult Index()
        {
            var tOrders = db.TOrders.Include(t => t.TBuyer).Include(t => t.TProduct);
            return View(tOrders.ToList());
        }

        // GET: Admin/TOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOrder tOrder = db.TOrders.Find(id);
            if (tOrder == null)
            {
                return HttpNotFound();
            }
            return View(tOrder);
        }

        // GET: Admin/TOrders/Create
        public ActionResult Create()
        {
            ViewBag.C_idBuyer = new SelectList(db.TBuyers, "idBuyer", "Firstname");
            ViewBag.C_idProduct = new SelectList(db.TProducts, "idProduct", "Name");
            return View();
        }

        // POST: Admin/TOrders/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOrder,Quantity,Date,C_idProduct,C_idBuyer")] TOrder tOrder)
        {
            if (ModelState.IsValid)
            {
                db.TOrders.Add(tOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_idBuyer = new SelectList(db.TBuyers, "idBuyer", "Firstname", tOrder.C_idBuyer);
            ViewBag.C_idProduct = new SelectList(db.TProducts, "idProduct", "Name", tOrder.C_idProduct);
            return View(tOrder);
        }

        // GET: Admin/TOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOrder tOrder = db.TOrders.Find(id);
            if (tOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_idBuyer = new SelectList(db.TBuyers, "idBuyer", "Firstname", tOrder.C_idBuyer);
            ViewBag.C_idProduct = new SelectList(db.TProducts, "idProduct", "Name", tOrder.C_idProduct);
            return View(tOrder);
        }

        // POST: Admin/TOrders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOrder,Quantity,Date,C_idProduct,C_idBuyer")] TOrder tOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_idBuyer = new SelectList(db.TBuyers, "idBuyer", "Firstname", tOrder.C_idBuyer);
            ViewBag.C_idProduct = new SelectList(db.TProducts, "idProduct", "Name", tOrder.C_idProduct);
            return View(tOrder);
        }

        // GET: Admin/TOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TOrder tOrder = db.TOrders.Find(id);
            if (tOrder == null)
            {
                return HttpNotFound();
            }
            return View(tOrder);
        }

        // POST: Admin/TOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TOrder tOrder = db.TOrders.Find(id);
            db.TOrders.Remove(tOrder);
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
