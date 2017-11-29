using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Mvc_Repository.Models;
using Mvc_Repository.Services.Interfaces;

namespace Mvc_Repository.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View(categoryService.GetAll().OrderByDescending(x => x.CategoryID).ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("index");
            }

            return View(categoryService.GetByID(id.Value));
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,Description,Picture")] Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Create(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(categoryService.GetByID(id.Value));
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,Description,Picture")] Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Update(category);

                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {

            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(categoryService.GetByID(id.Value));
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                this.categoryService.Delete(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id });
            }

            return RedirectToAction("Index");
        }
    }
}
