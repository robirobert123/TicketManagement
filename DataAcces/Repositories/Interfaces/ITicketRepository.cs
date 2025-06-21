using DataAcces.GenericRepository;
using System.Collections.Generic;

namespace DataAcces.Repositories.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        IEnumerable<Ticket> GetAllTickets();
        Ticket GetTicketById(int id);
        void InsertTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(int id);
        void DeleteAll();
    }
}
