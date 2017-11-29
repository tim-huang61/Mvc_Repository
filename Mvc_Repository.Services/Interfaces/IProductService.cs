using System.Collections.Generic;
using Mvc_Repository.Models;
using Mvc_Repository.Services.Misc;

namespace Mvc_Repository.Services.Interfaces
{
    public interface IProductService
    {
        IResult Create(Product instance);

        IResult Update(Product instance);

        IResult Delete(int productID);

        bool IsExists(int productID);

        Product GetByID(int productID);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetByCategory(int categoryID);
    }
}