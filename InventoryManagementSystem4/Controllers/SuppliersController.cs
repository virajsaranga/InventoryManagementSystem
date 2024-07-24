using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using InventoryManagementSystem4.Models;

namespace InventoryManagementSystem4.Controllers
{
    public class SuppliersController : Controller
    {
        private InventoryDbContext db = new InventoryDbContext();

        // GET: Suppliers
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Suppliers_Read([DataSourceRequest] DataSourceRequest request)
        {
            var suppliers = db.Suppliers.ToList();
            return Json(suppliers.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Suppliers_Create([DataSourceRequest] DataSourceRequest request, Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
            }
            return Json(new[] { supplier }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Suppliers_Update([DataSourceRequest] DataSourceRequest request, Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new[] { supplier }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Suppliers_Destroy([DataSourceRequest] DataSourceRequest request, Supplier supplier)
        {
            if (supplier != null)
            {
                db.Entry(supplier).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return Json(new[] { supplier }.ToDataSourceResult(request, ModelState));
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
