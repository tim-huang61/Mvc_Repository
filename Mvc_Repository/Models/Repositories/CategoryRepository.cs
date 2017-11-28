using Mvc_Repository.Models.Interfaces;

namespace Mvc_Repository.Models.Repositories
{
    internal class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public Category GetByID(int categoryID)
        {
            return Get(x => x.CategoryID == categoryID);
        }
    }
}