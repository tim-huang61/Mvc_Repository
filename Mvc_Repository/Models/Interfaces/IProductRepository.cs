using System;
using System.Linq;

namespace Mvc_Repository.Models.Interfaces
{
    public interface IProductRepository : IDisposable
    {
        void Create(Product product);

        void Update(Product product);

        void Delete(Product product);

        Product Get(int productId);

        IQueryable<Product> GetAll();

        void SaveChanges();
    }
}