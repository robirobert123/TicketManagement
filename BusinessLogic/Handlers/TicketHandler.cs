using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Handlers.Interfaces;
using DataAcces;
using DataAcces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Handlers
{
    public class TicketHandler : ITicketHandler
    {
        public ResultEntity<IEnumerable<TicketEntity>> GetAllTickets()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var list = unitOfWork.TicketRepository.GetAllTickets().ToList().ToBusinessEntity();

                    return new ResultEntity<IEnumerable<TicketEntity>> { Data = list, IsSuccess = true, Message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new ResultEntity<IEnumerable<TicketEntity>> { Data = null, IsSuccess = false, Message = e.Message };
            }
        }
        public ResultEntity<TicketEntity> GetTicketById(int ticketId)
        {
            try
            {
                using (
                var unitOfWork = new UnitOfWork())
                {
                    var ticket = unitOfWork.TicketRepository.Get(ticketId).ToBusinessEntity();

                    return new ResultEntity<TicketEntity> { Data = ticket, IsSuccess = true, Message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new ResultEntity<TicketEntity> { Data = null, IsSuccess = false, Message = e.Message };
            }
        }
        public ResultEntity<IEnumerable<StatusEntity>> GetTicketStatuses()
        {
            try
            {
                using (
                var unitOfWork = new UnitOfWork())
                {
                    var status = unitOfWork.StatusRepository.GetAllStatus().ToList().ToBusinessEntity();

                    return new ResultEntity<IEnumerable<StatusEntity>> { Data = status, IsSuccess = true, Message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new ResultEntity<IEnumerable<StatusEntity>> { Data = null, IsSuccess = false, Message = e.Message };
            }
        }
        public ResultEntity<IEnumerable<PriorityEntity>> GetTicketPriorities()
        {
            try
            {
                using (
                var unitOfWork = new UnitOfWork())
                {
                    var priority = unitOfWork.PriorityRepository.GetAllPriorities().ToList().ToBusinessEntity();

                    return new ResultEntity<IEnumerable<PriorityEntity>> { Data = priority, IsSuccess = true, Message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new ResultEntity<IEnumerable<PriorityEntity>> { Data = null, IsSuccess = false, Message = e.Message };
            }
        }
        public ResultEntity<IEnumerable<CategoryEntity>> GetTicketCategories()
        {
            try
            {
                using (
                var unitOfWork = new UnitOfWork())
                {
                    var category = unitOfWork.CategoryRepository.GetAllCategories().ToList().ToBusinessEntity();

                    return new ResultEntity<IEnumerable<CategoryEntity>> { Data = category, IsSuccess = true, Message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new ResultEntity<IEnumerable<CategoryEntity>> { Data = null, IsSuccess = false, Message = e.Message };
            }

        }
        public ResultEntity<IEnumerable<UserEntity>> GetTicketUsers()
        {
            try
            {
                using (
            var unitOfWork = new UnitOfWork())
                {
                    var user = unitOfWork.UserRepository.GetAllUsers().ToList().ToBusinessEntity();

                    return new ResultEntity<IEnumerable<UserEntity>> { Data = user, IsSuccess = true, Message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new ResultEntity<IEnumerable<UserEntity>> { Data = null, IsSuccess = false, Message = e.Message };
            }
        }
        public ResultEntity<TicketEntity> InsertTicket(Ticket data)
        {
            try
            {
                using (
                var unitOfWork = new UnitOfWork())
                {
                    unitOfWork.TicketRepository.InsertTicket(data);
                    unitOfWork.Save();

                    return new ResultEntity<TicketEntity> { Data = data.ToBusinessEntity(), IsSuccess = true, Message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new ResultEntity<TicketEntity> { Data = null, IsSuccess = false, Message = e.Message };
            }

        }
        public ResultEntity<TicketEntity> UpdateTicket(Ticket data)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    unitOfWork.TicketRepository.UpdateTicket(data);
                    unitOfWork.Save();
                }
                return new ResultEntity<TicketEntity> { Data = data.ToBusinessEntity(), IsSuccess = true, Message = "Succes" };
            }
            catch (Exception e)
            {
                return new ResultEntity<TicketEntity> { Data = null, IsSuccess = false, Message = e.Message };
            }


        }
        public ResultEntity<CommentEntity> InsertTicketComment(Comment data)
        {
            try
            {
                using (
                var unitOfWork = new UnitOfWork())
                {
                    unitOfWork.CommentRepository.InsertComment(data);
                    unitOfWork.Save();

                    return new ResultEntity<CommentEntity> { Data = data.ToBusinessEntity(), IsSuccess = true, Message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new ResultEntity<CommentEntity> { Data = null, IsSuccess = false, Message = e.Message };
            }

        }
        public ResultEntity<TicketEntity> DeleteTicket(int ticketId)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                {
                    var ticket = unitOfWork.TicketRepository.Get(ticketId);
                    if (ticket == null)
                    {
                        return new ResultEntity<TicketEntity> { Data = null, IsSuccess = false, Message = "Ticket not found" };
                    }

                    unitOfWork.TicketRepository.Delete(ticketId);
                    unitOfWork.Save();

                    return new ResultEntity<TicketEntity> { Data = null, IsSuccess = true, Message = "Success" };
                }
            }
            catch (Exception e)
            {
                return new ResultEntity<TicketEntity> { Data = null, IsSuccess = false, Message = e.Message };
            }
        }
    }
}
