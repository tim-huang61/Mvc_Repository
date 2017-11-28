using System.Collections.Generic;
using System.Linq;
using Mvc_Repository.Models.Interfaces;

namespace Mvc_Repository.Models.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public Product GetByID(int productID)
        {
            return Get(p => p.ProductID == productID);
        }

        public IEnumerable<Product> GetByCateogy(int categoryID)
        {
            return GetAll().Where(p => p.CategoryID == categoryID);
        }
    }
}