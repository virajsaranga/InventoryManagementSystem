using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using InventoryManagementSystem4.Models;
using InventoryManagementSystem4.DTOs;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Data.Entity.Infrastructure;

namespace InventoryManagementSystem4.Controllers
{
    public class ProductsController : Controller
    {
        
        private readonly InventoryDbContext db = new InventoryDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var categories = db.Categories
                .Select(c => new { c.CategoryID, c.CategoryName })
                .ToList();
            ViewData["categories"] = categories;

            var suppliers = db.Suppliers
                .Select(s => new { s.SupplierID, s.SupplierName })
                .ToList();
            ViewData["suppliers"] = suppliers;

            return View(); 
        }

        // GET: Products/Read
        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier)
                .Select(p => new ProductDTO
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    CategoryID = p.CategoryID,
                    CategoryName = p.Category.CategoryName,
                    SupplierID = p.SupplierID,
                    SupplierName = p.Supplier.SupplierName,
                    QuantityPerUnit = p.QuantityPerUnit,
                    UnitPrice = p.UnitPrice,
                    UnitsInStock = p.UnitsInStock
                }).ToList();

            return Json(products.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // POST: Products/Create
        [HttpPost]
        
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = new Product
                    {
                        ProductName = productDTO.ProductName,
                        CategoryID = productDTO.CategoryID,
                        SupplierID = productDTO.SupplierID,
                        QuantityPerUnit = productDTO.QuantityPerUnit,
                        UnitPrice = productDTO.UnitPrice,
                        UnitsInStock = productDTO.UnitsInStock
                    };

                    db.Products.Add(product);
                    db.SaveChanges();

                    productDTO.ProductID = product.ProductID;

                    var category = db.Categories.Find(product.CategoryID);
                    productDTO.CategoryName = category?.CategoryName ?? "Unknown Category";

                    var supplier = db.Suppliers.Find(product.SupplierID);
                    productDTO.SupplierName = supplier?.SupplierName ?? "Unknown Supplier";

                    return Json(new[] { productDTO }.ToDataSourceResult(request, ModelState));
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    System.Diagnostics.Debug.WriteLine($"DbUpdateException in Create: {ex.Message}");
                    if (ex.InnerException != null)
                        System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while processing your request.");
                    System.Diagnostics.Debug.WriteLine($"Exception in Create: {ex.Message}");
                    if (ex.InnerException != null)
                        System.Diagnostics.Debug.WriteLine($"Inner exception: {ex.InnerException.Message}");

                    return Content($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                }
            }

          
            return Json(new DataSourceResult
            {
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray()
            });
        }
        // POST: Products/Update
        [HttpPost]
       
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(productDTO.ProductID);

                if (product != null)
                {
                    product.ProductName = productDTO.ProductName;
                    product.CategoryID = productDTO.CategoryID;
                    product.SupplierID = productDTO.SupplierID;
                    product.QuantityPerUnit = productDTO.QuantityPerUnit;
                    product.UnitPrice = productDTO.UnitPrice;
                    product.UnitsInStock = productDTO.UnitsInStock;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(new[] { productDTO }.ToDataSourceResult(request, ModelState));
        }

        // POST: Products/Destroy
        [HttpPost]
       
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, ProductDTO productDTO)
        {
            if (productDTO != null)
            {
                var product = db.Products.Find(productDTO.ProductID);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
            }
            return Json(new[] { productDTO }.ToDataSourceResult(request, ModelState));
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
