using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToyStore.Models;

namespace ToyStore.Controllers
{
    public class PurchasesController : Controller
    {
        private ToyStoreContext db = new ToyStoreContext();

        [Authorize(Roles = "user, admin")]
        public ActionResult Purchase(TOrder order)
        {
            order.C_idBuyer = int.Parse(HttpContext.Request.Cookies["idBuyer"].Value);
            order.Date = DateTime.Now;
            
            db.TOrders.Add(order);
            db.SaveChanges();

            return Redirect("/Home/index");
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