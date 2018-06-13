using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToyStore.Areas.Access.Providers;
using ToyStore.Models;

namespace ToyStore.Controllers
{
    public class HomeController : Controller
    {

        private ToyStoreContext db = new ToyStoreContext();

        // GET: Home
        public ActionResult Index(int? idcategorie)
        {
            string str;
            IQueryable<TProduct> tProducts;

            if (idcategorie == null)
            {
                tProducts = db.TProducts
                    .Include(t => t.TCategorie)
                    .Include(t => t.TSubcategory);
            }
            else
            {
                tProducts = db.TProducts
                      .Where(t => t.TCategorie.idCategorie == idcategorie)
                      .Include(t => t.TCategorie)
                      .Include(t => t.TSubcategory);
            }


            foreach (var product in tProducts)
            {
                str = product.Description;
                if (str != null && str.Length > 20)
                    str = str.Substring(0, 40) + "...";
                product.SmallDescription = str;
            }
            return View(tProducts.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TProduct tProduct = db.TProducts.Find(id);
            if (tProduct == null)
            {
                return HttpNotFound();
            }
            return View(tProduct);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            ViewBag.Massage = "Ты вошол";
            return View();
        }
    }
}

