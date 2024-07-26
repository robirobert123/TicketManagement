using DataAcces.GenericRepository;
using DataAcces.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAcces.Repositories
{
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        private readonly TicketManagementEntities _context;

        public StatusRepository(TicketManagementEntities context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Status> GetAllStatus()
        {
            return _context.Status.ToList();
        }
        public Status GetStatusById(int id)
        {
            return _context.Status.Find(id);
        }
        public void InsertStatus(Status status)
        {
            _context.Status.Add(status);
        }
        public void UpdateStatus(Status status)
        {
            _context.Entry(status).State = System.Data.Entity.EntityState.Modified;
        }
        public void DeleteStatus(int id)
        {
            Status status = _context.Status.Find(id);
            _context.Status.Remove(status);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void DeleteAll()
        {
            _context.Status.RemoveRange(_context.Status);
        }
    }
}

