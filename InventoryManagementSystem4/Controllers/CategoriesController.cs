using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using InventoryManagementSystem4.Models;

namespace InventoryManagementSystem4.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly InventoryDbContext db = new InventoryDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        // GET: Categories/Read
        public ActionResult Categories_Read([DataSourceRequest] DataSourceRequest request)
        {
            var categories = db.Categories.ToList();
            return Json(categories.ToDataSourceResult(request));
        }

        // POST: Categories/Create
        [HttpPost]
        public ActionResult Categories_Create([DataSourceRequest] DataSourceRequest request, Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        // POST: Categories/Update
        [HttpPost]
        public ActionResult Categories_Update([DataSourceRequest] DataSourceRequest request, Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        // POST: Categories/Destroy
        [HttpPost]
        public ActionResult Categories_Destroy([DataSourceRequest] DataSourceRequest request, Category category)
        {
            if (category != null)
            {
                db.Entry(category).State = EntityState.Deleted;
                db.SaveChanges();
            }
            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
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
