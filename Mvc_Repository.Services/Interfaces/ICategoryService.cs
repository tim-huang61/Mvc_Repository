using System.Collections.Generic;
using Mvc_Repository.Models;
using Mvc_Repository.Services.Misc;

namespace Mvc_Repository.Services.Interfaces
{
    public interface ICategoryService
    {
        IResult Create(Category instance);

        IResult Update(Category instance);

        IResult Delete(int categoryID);

        bool IsExists(int categoryID);

        Category GetByID(int categoryID);

        IEnumerable<Category> GetAll();
    }
}