using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToyStore.Models;
using System.IO;

namespace ToyStore.Areas.Admin.Controllers
{
    public class TProductsController : Controller
    {
        private ToyStoreContext db = new ToyStoreContext();

        // GET: Admin/TProducts
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

        // GET: Admin/TProducts/Details/5
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

        // GET: Admin/TProducts/Create
        public ActionResult Create()
        {
            ViewBag.C_idCategorie = new SelectList(db.TCategories, "idCategorie", "Category");
            ViewBag.C_idSubcategory = new SelectList(db.TSubcategories, "idSubcategory", "Subcategory");
            return View();
        }

        // POST: Admin/TProducts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProduct,Name,Price,Description,Image,Picture,Manufacturer,C_idSubcategory,C_idCategorie")] TProduct tProduct, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                tProduct.Picture = imageData;
                db.TProducts.Add(tProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_idCategorie = new SelectList(db.TCategories, "idCategorie", "Category", tProduct.C_idCategorie);
            ViewBag.C_idSubcategory = new SelectList(db.TSubcategories, "idSubcategory", "Subcategory", tProduct.C_idSubcategory);
            return View(tProduct);
        }

        // GET: Admin/TProducts/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.C_idCategorie = new SelectList(db.TCategories, "idCategorie", "Category", tProduct.C_idCategorie);
            ViewBag.C_idSubcategory = new SelectList(db.TSubcategories, "idSubcategory", "Subcategory", tProduct.C_idSubcategory);
            return View(tProduct);
        }

        // POST: Admin/TProducts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idProduct,Name,Price,Description,Image,Picture,Manufacturer,C_idSubcategory,C_idCategorie")] TProduct tProduct, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                tProduct.Picture = imageData;
                db.Entry(tProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_idCategorie = new SelectList(db.TCategories, "idCategorie", "Category", tProduct.C_idCategorie);
            ViewBag.C_idSubcategory = new SelectList(db.TSubcategories, "idSubcategory", "Subcategory", tProduct.C_idSubcategory);
            return View(tProduct);
        }

        // GET: Admin/TProducts/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Admin/TProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TProduct tProduct = db.TProducts.Find(id);
            db.TProducts.Remove(tProduct);
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
