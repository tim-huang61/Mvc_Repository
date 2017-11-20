using System;
using System.Data.Entity;
using System.Linq;
using Mvc_Repository.Models.Interfaces;

namespace Mvc_Repository.Models.Repositories
{
    internal class CategoryRepository : ICategoryRepository, IDisposable
    {
        private NorthwindEntities db;

        public CategoryRepository()
        {
            this.db = new NorthwindEntities();
        }

        public void Create(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            db.Categories.Add(category);
            this.SaveChanges();
        }

        public void Update(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            db.Entry(category).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void Delete(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            db.Entry(category).State = EntityState.Deleted;
            this.SaveChanges();
        }

        public Category Get(int categoryId)
        {
            return db.Categories.FirstOrDefault(x => x.CategoryID == categoryId);
        }

        public IQueryable<Category> GetAll()
        {
            return db.Categories.OrderBy(x => x.CategoryID);
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}