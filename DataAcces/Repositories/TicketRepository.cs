using DataAcces.GenericRepository;
using DataAcces.Repositories.Interfaces;
using System.Linq;

namespace DataAcces.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly TicketManagementEntities _context;

        public TicketRepository(TicketManagementEntities context) : base(context)
        {
            _context = context;
        }
        public Ticket GetTicketById(int id)
        {
            return _context.Tickets.Include("Category").FirstOrDefault(t => t.TicketID == id);
        }
        public void InsertTicket(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
        }
        public void UpdateTicket(Ticket ticket)
        {
            _context.Entry(ticket).State = System.Data.Entity.EntityState.Modified;
        }
        public void DeleteTicket(int id)
        {
            Ticket ticket = _context.Tickets.Find(id);
            _context.Tickets.Remove(ticket);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void DeleteAll()
        {
            _context.Tickets.RemoveRange(_context.Tickets);
        }
    }
}
