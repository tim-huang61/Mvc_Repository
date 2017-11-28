using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Mvc_Repository.Models;
using Mvc_Repository.Models.Interfaces;
using Mvc_Repository.Models.Repositories;

namespace Mvc_Repository.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        public IEnumerable<Category> Categories => categoryRepository.GetAll();

        public ProductsController()
        {
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(productRepository.GetAll().OrderByDescending(x => x.ProductID).ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(productRepository.GetByID(id.Value));
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
                productRepository.Create(product);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "CategoryName", product.CategoryID);

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id = 0)
        {
            var product = this.productRepository.Get(p => p.CategoryID == id);
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
                this.productRepository.Update(product);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(Categories, "CategoryID", "CategoryName", product.CategoryID);

            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            var product = this.productRepository.GetByID(id);
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
            var product = this.productRepository.GetByID(id);
            this.productRepository.Delete(product);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.productRepository.Dispose();
                this.categoryRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
