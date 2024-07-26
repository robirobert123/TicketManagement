using DataAcces.GenericRepository;
using DataAcces.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAcces.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly TicketManagementEntities _context;

        public CategoryRepository(TicketManagementEntities context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
        public Category GetCategoryByID(int id)
        {
            return _context.Categories.Find(id);
        }
        public void InsertCategory(Category category)
        {
            _context.Categories.Add(category);
        }
        public void UpdateCategory(Category category)
        {
            _context.Entry(category).State = System.Data.Entity.EntityState.Modified;
        }
        public void DeleteCategory(int id)
        {
            Category category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
        }
        public void DeleteAll()
        {
            _context.Categories.RemoveRange(_context.Categories);
        }
    }
}

