namespace Mvc_Repository.Models.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetByID(int categoryID);
    }
}