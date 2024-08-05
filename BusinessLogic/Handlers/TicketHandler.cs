using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Handlers.Interfaces;
using DataAcces;
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
        public ResultEntity<IEnumerable<StatusEntity>> GetTicketStatuses()
        {
            using (
                var unitOfWork = new UnitOfWork())
            {
                var status = unitOfWork.StatusRepository.GetAllStatus().ToList().ToBusinessEntity();

                return new ResultEntity<IEnumerable<StatusEntity>> { Data = status, IsSuccess = true, Message = "Success" };
            }
        }
        public ResultEntity<IEnumerable<PriorityEntity>> GetTicketPriorities()
        {
            using (
                var unitOfWork = new UnitOfWork())
            {
                var priority = unitOfWork.PriorityRepository.GetAllPriorities().ToList().ToBusinessEntity();

                return new ResultEntity<IEnumerable<PriorityEntity>> { Data = priority, IsSuccess = true, Message = "Success" };
            }
        }
        public ResultEntity<IEnumerable<CategoryEntity>> GetTicketCategories()
        {
            using (
                var unitOfWork = new UnitOfWork())
            {
                var category = unitOfWork.CategoryRepository.GetAllCategories().ToList().ToBusinessEntity();

                return new ResultEntity<IEnumerable<CategoryEntity>> { Data = category, IsSuccess = true, Message = "Success" };
            }
        }
        public ResultEntity<IEnumerable<UserEntity>> GetTicketUsers()
        {
            using (
                var unitOfWork = new UnitOfWork())
            {
                var user = unitOfWork.UserRepository.GetAllUsers().ToList().ToBusinessEntity();

                return new ResultEntity<IEnumerable<UserEntity>> { Data = user, IsSuccess = true, Message = "Success" };
            }
        }
        public ResultEntity<TicketEntity> InsertTicket(Ticket data)
        {
            using (
                var unitOfWork = new UnitOfWork())
            {
                unitOfWork.TicketRepository.InsertTicket(data);
                unitOfWork.Save();

                return new ResultEntity<TicketEntity> { Data = data.ToBusinessEntity(), IsSuccess = true, Message = "Success" };
            }
        }
        public ResultEntity<TicketEntity> UpdateTicket(Ticket data)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                unitOfWork.TicketRepository.UpdateTicket(data);
                unitOfWork.Save();
            }
            return new ResultEntity<TicketEntity> { Data = data.ToBusinessEntity(), IsSuccess = true, Message = "Succes" };

        }
    }
}
