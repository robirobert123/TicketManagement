using DataAcces.GenericRepository;
using System.Collections.Generic;

namespace DataAcces.Repositories.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryByID(int categoryId);
        void InsertCategory(Category category);
        void DeleteCategory(int categoryId);
        void UpdateCategory(Category category);
        void DeleteAll();
    }
}
