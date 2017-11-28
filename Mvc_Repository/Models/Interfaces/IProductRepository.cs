using System.Collections.Generic;

namespace Mvc_Repository.Models.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetByID(int productID);

        IEnumerable<Product> GetByCateogy(int categoryID);
    }
}