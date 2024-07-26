using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Handlers.Interfaces
{
    public interface ITicketHandler
    {
        ResultEntity<IEnumerable<TicketEntity>> GetAllTickets();
        ResultEntity<TicketEntity> GetTicketById(int ticketId);
    }
}
