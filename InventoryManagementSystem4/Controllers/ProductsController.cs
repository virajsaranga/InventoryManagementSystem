using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using InventoryManagementSystem4.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace InventoryManagementSystem4.Controllers
{
    public class ProductsController : Controller
    {
        private readonly InventoryDbContext db = new InventoryDbContext();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        // GET: Products/Read
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var products = db.Products
                .Include(p => p.Category)  
                .Include(p => p.Supplier)  
                .ToList();
            return Json(products.ToDataSourceResult(request));
        }

        // POST: Products/Create
        [HttpPost]
        
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        // POST: Products/Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        // POST: Products/Destroy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, Product product)
        {
            if (product != null)
            {
                db.Products.Attach(product);
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        // Dispose
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
