using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToyStore.Models;

namespace ToyStore.Controllers
{
    public class TOrdersController : Controller
    {
        private ToyStoreContext db = new ToyStoreContext();

        // GET: TOrders
        [Authorize(Roles = "user, admin")]
        public ActionResult Index()
        {
          
            List<decimal> totalAmount = new List<decimal>();
            decimal result = 0;

            var id = int.Parse(HttpContext.Request.Cookies["idBuyer"].Value);
            var tOrders = db.TOrders.Where(t => t.TBuyer.idBuyer == id);
                
                
            
            
            /*.Include(t => t.TBuyer).Include(t => t.TProduct)*/;
            foreach (var order in tOrders)
            {
                
                order.Amount = order.TProduct.Price * order.Quantity;
                
                totalAmount.Add(order.Amount);
                //a.amo = a.TProduct.Price * (int)a.Аmount;
            }
            foreach(var amount in totalAmount)
            {
                
                result += amount; 
            }
            ViewBag.totalAmount = result;
            return View(tOrders.ToList());
        }

        // GET: TOrders/Delete/5
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

        // POST: TOrders/Delete/5
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
