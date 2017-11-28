using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Mvc_Repository.Models.Interfaces;

namespace Mvc_Repository.Models.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext dbContext;

        public GenericRepository()
        {
            dbContext = new NorthwindEntities();
        }

        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this.dbContext.Set<TEntity>().Add(entity);
            this.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Queryable.FirstOrDefault(this.dbContext.Set<TEntity>(), predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Queryable.AsQueryable<TEntity>(this.dbContext.Set<TEntity>());
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
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
                if (this.dbContext != null)
                {
                    this.dbContext.Dispose();
                    this.dbContext = null;
                }
            }
        }
    }
}