using DataAcces.GenericRepository;
using DataAcces.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAcces.Repositories
{
    public class PriorityRepository : GenericRepository<Priority>, IPriorityRepository
    {
        private readonly TicketManagementEntities _context;

        public PriorityRepository(TicketManagementEntities context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Priority> GetAllPriorities()
        {
            return _context.Priorities.ToList();
        }

        public Priority GetPriorityById(int id)
        {
            return _context.Priorities.Find(id);
        }

        public void InsertPriority(Priority priority)
        {
            _context.Priorities.Add(priority);
        }

        public void UpdatePriority(Priority priority)
        {
            _context.Entry(priority).State = System.Data.Entity.EntityState.Modified;
        }
        public void DeletePriority(int id)
        {
            Priority priority = _context.Priorities.Find(id);
            _context.Priorities.Remove(priority);
        }
        public void DeleteAll()
        {
            _context.Priorities.RemoveRange(_context.Priorities);
        }
    }
}
