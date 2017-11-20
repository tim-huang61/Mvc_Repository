using System;
using System.Data.Entity;
using System.Linq;
using Mvc_Repository.Models.Interfaces;

namespace Mvc_Repository.Models.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private NorthwindEntities db;

        public ProductRepository()
        {
            this.db = new NorthwindEntities();
        }

        public void Create(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("category");
            }

            db.Products.Add(product);
            this.SaveChanges();
        }

        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("category");
            }

            db.Entry(product).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void Delete(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("category");
            }

            db.Entry(product).State = EntityState.Deleted;
            this.SaveChanges();
        }

        public Product Get(int productId)
        {
            return db.Products.FirstOrDefault(p => p.ProductID == productId);
        }

        public IQueryable<Product> GetAll()
        {
            return db.Products.Include(p => p.Category).OrderByDescending(x => x.ProductID);
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