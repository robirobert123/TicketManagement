using DataAcces.GenericRepository;

namespace DataAcces.Repositories.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        void InsertTicket(Ticket ticket);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(int id);
        void DeleteAll();
    }
}
