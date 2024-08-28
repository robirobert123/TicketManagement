using BusinessLogic.Constants;
using BusinessLogic.Entities;
using BusinessLogic.Handlers;
using DataAcces;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketManagement.Models;

namespace TicketManagement.Helpers
{
    public class TicketHelper
    {
        public TicketDetailsModel GetDetailModel(TicketEntity ticketEntity)
        {
            var result = new TicketDetailsModel();
            result.TicketID = ticketEntity.TicketID; ;
            result.Title = ticketEntity.Title;
            result.Description = ticketEntity.Description;
            result.PriorityID = ticketEntity.PriorityEntity.PriorityID;
            result.PriorityText = ticketEntity.PriorityEntity.PriorityName;
            result.CategoryID = ticketEntity.CategoryEntity.CategoryID;
            result.CategoryText = ticketEntity.CategoryEntity.CategoryName;
            result.StatusID = ticketEntity.StatusEntity.StatusID;
            result.StatusText = ticketEntity.StatusEntity.StatusName;
            result.CreatedDate = ticketEntity.created_Date;
            result.AuditDate = ticketEntity.audit_Date;
            result.Assignee = ticketEntity.AssigneeEntity.Id;
            result.AssigneeName = ticketEntity.AssigneeEntity.FirstName + " " + ticketEntity.AssigneeEntity.LastName;
            result.CreatedUser = ticketEntity.created_User;
            result.AuditUser = ticketEntity.audit_User;
            result.Comments = ticketEntity.Comments.ToList();
            return result;
        }
        public TicketCreateModel GetCreateModel()
        {

            var ticketHandler = new TicketHandler();
            var model = new TicketCreateModel();
            model.IsValid = true;

            try
            {
                var ticketStatusesResult = ticketHandler.GetTicketStatuses();
                var ticketCategoriesResult = ticketHandler.GetTicketCategories();
                var ticketPrioritiesResult = ticketHandler.GetTicketPriorities();
                var ticketUsersResult = ticketHandler.GetTicketUsers();

                if (!ticketStatusesResult.IsSuccess)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ticketStatusesResult.Message);
                }
                if (ticketStatusesResult.Data == null || ticketStatusesResult.Data.Count() == 0)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ErrorMessages.NO_TICKET_STATUSES_FOUND);
                }
                else
                {
                    model.TicketStatuses = ticketStatusesResult.Data
                        .Select(
                            s => new SelectListItem
                            {
                                Value = s.StatusID.ToString(),
                                Text = s.StatusName
                            })
                        .ToList();
                }

                if (!ticketPrioritiesResult.IsSuccess)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ticketPrioritiesResult.Message);
                }
                if (ticketPrioritiesResult.Data == null || ticketPrioritiesResult.Data.Count() == 0)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ErrorMessages.NO_TICKET_PRIORITIES_FOUND);
                }
                else
                {
                    model.TicketPriorities = ticketPrioritiesResult.Data
                        .Select(
                            s => new SelectListItem
                            {
                                Value = s.PriorityID.ToString(),
                                Text = s.PriorityName
                            })
                        .ToList();
                }

                if (!ticketCategoriesResult.IsSuccess)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ticketCategoriesResult.Message);
                }
                if (ticketCategoriesResult.Data == null || ticketCategoriesResult.Data.Count() == 0)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ErrorMessages.NO_TICKET_CATEGORIES_FOUND);
                }
                else
                {
                    model.TicketCategories = ticketCategoriesResult.Data
                        .Select(
                            s => new SelectListItem
                            {
                                Value = s.CategoryID.ToString(),
                                Text = s.CategoryName
                            })
                        .ToList();
                }

                if (!ticketUsersResult.IsSuccess)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ticketUsersResult.Message);
                }
                if (ticketUsersResult == null || ticketUsersResult.Data.Count() == 0)
                {
                    model.IsValid = true;
                    model.ErrorMessages.Add(ErrorMessages.NO_TICKET_USERS_FOUND);
                }
                else
                {
                    model.TicketAssignees = ticketUsersResult.Data
                    .Select(
                        s => new SelectListItem
                        {
                            Value = s.Id,
                            Text = s.FirstName + " " + s.LastName
                        })
                    .ToList();
                }
            }
            catch (Exception e)
            {
                model.IsValid = false;
                model.ErrorMessages.Add(e.Message);
            }
            return model;
        }
        public Ticket ToDataEntity(TicketCreateModel ticketModel, string user)
        {
            var result = new Ticket();
            result.Title = ticketModel.Title;
            result.Description = ticketModel.Description;
            result.PriorityID = ticketModel.PriorityID;
            result.StatusID = ticketModel.StatusID;
            result.CategoryID = ticketModel.CategoryID;
            result.Assignee = ticketModel.Assignee;
            result.Deleted = false;
            result.created_Date = DateTime.Now;
            result.audit_Date = DateTime.Now;
            result.created_User = user;
            result.audit_User = user;
            return result;
        }

        public TicketEditModel GetEditModel(TicketEntity ticket)
        {

            var model = new TicketEditModel();
            var ticketHandler = new TicketHandler();
            model.Title = ticket.Title;
            model.Description = ticket.Description;
            model.CreatedDate = ticket.created_Date;
            model.CreatedUser = ticket.created_User;
            model.CategoryID = ticket.CategoryEntity.CategoryID;
            model.TicketID = ticket.TicketID;
            model.PriorityID = ticket.PriorityEntity.PriorityID;
            model.StatusID = ticket.StatusEntity.StatusID;
            model.Assignee = ticket.AssigneeEntity.Id;

            try
            {
                var ticketStatusesResult = ticketHandler.GetTicketStatuses();
                if (!ticketStatusesResult.IsSuccess)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ticketStatusesResult.Message);
                }
                if (ticketStatusesResult.Data == null || ticketStatusesResult.Data.Count() == 0)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ErrorMessages.NO_TICKET_STATUSES_FOUND);
                }
                else
                {
                    model.TicketStatuses = ticketStatusesResult.Data
                        .Select(
                            s => new SelectListItem
                            {
                                Value = s.StatusID.ToString(),
                                Text = s.StatusName
                            })
                    .ToList();
                }

                var ticketPrioritiesResult = ticketHandler.GetTicketPriorities();
                if (!ticketPrioritiesResult.IsSuccess)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ticketPrioritiesResult.Message);
                }
                if (ticketPrioritiesResult.Data == null || ticketPrioritiesResult.Data.Count() == 0)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ErrorMessages.NO_TICKET_PRIORITIES_FOUND);
                }
                else
                {
                    model.TicketPriorities = ticketPrioritiesResult.Data
                        .Select(
                            s => new SelectListItem
                            {
                                Value = s.PriorityID.ToString(),
                                Text = s.PriorityName
                            })
                    .ToList();
                }

                var ticketCategoriesResult = ticketHandler.GetTicketCategories();
                if (!ticketCategoriesResult.IsSuccess)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ticketCategoriesResult.Message);
                }
                if (ticketCategoriesResult.Data == null || ticketCategoriesResult.Data.Count() == 0)
                {
                    model.IsValid = false;
                    model.ErrorMessages.Add(ErrorMessages.NO_TICKET_CATEGORIES_FOUND);
                }
                else
                {
                    model.TicketCategories = ticketCategoriesResult.Data
                        .Select(
                            s => new SelectListItem
                            {
                                Value = s.CategoryID.ToString(),
                                Text = s.CategoryName
                            })
                    .ToList();
                }

                var ticketUsers = ticketHandler.GetTicketUsers().Data;
                model.TicketAssignees = ticketUsers
                    .Select(
                        s => new SelectListItem
                        {
                            Value = s.Id,
                            Text = s.FirstName + " " + s.LastName
                        })
                    .ToList();
            }
            catch (Exception e)
            {
                model.IsValid = false;
                model.ErrorMessages.Add(e.Message);
            }
            return model;
        }
        public Ticket ToDataEntity(TicketEditModel ticketModel, string user)
        {
            var result = new Ticket();
            result.Title = ticketModel.Title;
            result.TicketID = ticketModel.TicketID;
            result.Description = ticketModel.Description;
            result.PriorityID = ticketModel.PriorityID;
            result.StatusID = ticketModel.StatusID;
            result.CategoryID = ticketModel.CategoryID;
            result.Assignee = ticketModel.Assignee;
            result.created_Date = ticketModel.CreatedDate;
            result.audit_Date = DateTime.Now;
            result.created_User = ticketModel.CreatedUser;
            result.audit_User = user;
            return result;
        }
    }
}
