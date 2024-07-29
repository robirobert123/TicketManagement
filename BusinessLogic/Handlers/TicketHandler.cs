using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Handlers.Interfaces;
using DataAcces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Handlers
{
    public class TicketHandler : ITicketHandler
    {
        public ResultEntity<IEnumerable<TicketEntity>> GetAllTickets()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var list = unitOfWork.TicketRepository.GetAll().ToList().ToBusinessEntity();


                return new ResultEntity<IEnumerable<TicketEntity>> { Data = list, IsSuccess = true, Message = "Success" };
            }
        }
        public ResultEntity<TicketEntity> GetTicketById(int ticketId)
        {
            using (
                var unitOfWork = new UnitOfWork())
            {
                var ticket = unitOfWork.TicketRepository.Get(ticketId).ToBusinessEntity();

                return new ResultEntity<TicketEntity> { Data = ticket, IsSuccess = true, Message = "Success" };
            }
        }
    }
}
