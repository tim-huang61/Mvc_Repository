using System;
using System.Linq;
using System.Linq.Expressions;

namespace Mvc_Repository.Models.Interfaces
{
    public interface IRepository<TEntity> : IDisposable
    {
        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        void SaveChanges();
    }
}