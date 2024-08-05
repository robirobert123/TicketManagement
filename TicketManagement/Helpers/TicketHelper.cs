using BusinessLogic.Entities;
using BusinessLogic.Handlers;
using DataAcces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TicketManagement.Models;

namespace TicketManagement.Helpers
{
    public class TicketHelper
    {
        public TicketDetailsModel ToViewModel(TicketEntity ticketEntity)
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
            return result;
        }
        public TicketCreateModel ToCreateModel()
        {
            var ticketHandler = new TicketHandler();

            var ticketStatuses = ticketHandler.GetTicketStatuses().Data;
            var ticketPriorities = ticketHandler.GetTicketPriorities().Data;
            var ticketCategories = ticketHandler.GetTicketCategories().Data;
            var ticketUsers = ticketHandler.GetTicketUsers().Data;

            var model = new TicketCreateModel();
            model.TicketPriorities = ticketPriorities.Select(
                    s => new SelectListItem
                    {
                        Value = s.PriorityID.ToString(),
                        Text = s.PriorityName
                    }).ToList();

            model.TicketStatuses = ticketStatuses.Select(
                    s => new SelectListItem
                    {
                        Value = s.StatusID.ToString(),
                        Text = s.StatusName
                    }).ToList();

            model.TicketCategories = ticketCategories
                .Select(
                    s => new SelectListItem
                    {
                        Value = s.CategoryID.ToString(),
                        Text = s.CategoryName
                    })
                .ToList();

            model.TicketAssignees = ticketUsers
                .Select(
                    s => new SelectListItem
                    {
                        Value = s.Id,
                        Text = s.FirstName + " " + s.LastName
                    })
                .ToList();


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

        public TicketEditModel ToEditModel(TicketEntity ticket)
        {

            var model = new TicketEditModel();
            model.Title = ticket.Title;
            model.Description = ticket.Description;
            model.CreatedDate = ticket.created_Date;
            model.CreatedUser = ticket.created_User;
            model.CategoryID  = ticket.CategoryEntity.CategoryID; 
            model.TicketID = ticket.TicketID;
            model.PriorityID = ticket.PriorityEntity.PriorityID;
            model.StatusID = ticket.StatusEntity.StatusID;
            model.Assignee = ticket.AssigneeEntity.Id;


            var ticketHandler = new TicketHandler();

            var ticketStatuses = ticketHandler.GetTicketStatuses().Data;
            var ticketPriorities = ticketHandler.GetTicketPriorities().Data;
            var ticketCategories = ticketHandler.GetTicketCategories().Data;
            var ticketUsers = ticketHandler.GetTicketUsers().Data;

            model.TicketPriorities = ticketPriorities.Select(
                    s => new SelectListItem
                    {
                        Value = s.PriorityID.ToString(),
                        Text = s.PriorityName
                    }).ToList();
            model.TicketPriorities.First(x => x.Value == ticket.PriorityEntity.PriorityID.ToString()).Selected = true;

            model.TicketStatuses = ticketStatuses.Select(
                    s => new SelectListItem
                    {
                        Value = s.StatusID.ToString(),
                        Text = s.StatusName
                    }).ToList();
            model.TicketStatuses.First(x => x.Value == ticket.StatusEntity.StatusID.ToString()).Selected = true;


            model.TicketCategories = ticketCategories
                .Select(
                    s => new SelectListItem
                    {
                        Value = s.CategoryID.ToString(),
                        Text = s.CategoryName
                    })
                .ToList();
            model.TicketCategories.First(x => x.Value == ticket.CategoryEntity.CategoryID.ToString()).Selected = true;


            model.TicketAssignees = ticketUsers
                .Select(
                    s => new SelectListItem
                    {
                        Value = s.Id,
                        Text = s.FirstName + " " + s.LastName
                    })
                .ToList();
            model.TicketAssignees.First(x => x.Value == ticket.AssigneeEntity.Id).Selected = true;



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
