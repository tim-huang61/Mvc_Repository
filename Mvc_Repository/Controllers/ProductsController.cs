using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Mvc_Repository.Models;
using Mvc_Repository.Services;
using Mvc_Repository.Services.Interfaces;

namespace Mvc_Repository.Web.Controllers
{
    public class ProductsController : Controller
    {
        private IProductService productService;
        private ICategoryService categoryService;
        public IEnumerable<Category> Categories => categoryService.GetAll();

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        // GET: Products
        public ActionResult Index(string category = "all")
        {
            int categoryID = 1;
            ViewBag.CategorySelectList = int.TryParse(category, out categoryID)
                ? this.CategorySelectList(categoryID.ToString())
                : this.CategorySelectList("all");

            var result = category.Equals("all", StringComparison.OrdinalIgnoreCase)
              ? productService.GetAll()
              : productService.GetByCategory(categoryID);

            var products = result.OrderByDescending(x => x.ProductID).ToList();

            ViewBag.Category = category;

            return View(products);
        }

        public List<SelectListItem> CategorySelectList(string selectedValue = "all")
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = "All Category",
                    Value = "all",
                    Selected = selectedValue.Equals("all", StringComparison.OrdinalIgnoreCase)
                }
            };

            var categories = this.categoryService.GetAll().OrderBy(x => x.CategoryID);
            foreach (var c in categories)
            {
                items.Add(new SelectListItem
                {
                    Text = c.CategoryName,
                    Value = c.CategoryID.ToString(),
                    Selected = selectedValue.Equals(c.CategoryID.ToString())
                });
            }

            return items;
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(productService.GetByID(id.Value));
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                productService.Create(product);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "CategoryName", product.CategoryID);

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id = 0)
        {
            var product = this.productService.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
        
            ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "CategoryName", product.CategoryID);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (ModelState.IsValid)
            {
                this.productService.Update(product);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "CategoryName", product.CategoryID);

            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var product = this.productService.GetByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.productService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
