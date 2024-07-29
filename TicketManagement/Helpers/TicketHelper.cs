using BusinessLogic.Entities;
using TicketManagement.Models;

namespace TicketManagement.Helpers
{
    public class TicketHelper
    {
        public TicketModel ToViewModel(TicketEntity ticketEntity)
        {
            var result = new TicketModel();
            result.TicketID = ticketEntity.TicketID; ;
            result.Title = ticketEntity.Title;
            result.Description = ticketEntity.Description;
            result.PriorityID = ticketEntity.PriorityEntity.PriorityID;
            result.PriorityText = ticketEntity.PriorityEntity.PriorityName;
            result.CategoryID = ticketEntity.CategoryEntity.CategoryID;
            result.CategoryText = ticketEntity.CategoryEntity.CategoryName;
            result.StatusID = ticketEntity.StatusEntity.StatusID;
            result.StatusText = ticketEntity.StatusEntity.StatusName;
            result.created_Date = ticketEntity.created_Date;
            result.audit_Date = ticketEntity.audit_Date;
            result.Assignee = ticketEntity.AssigneeEntity.Id;
            result.AssigneeName = ticketEntity.AssigneeEntity.FirstName + " " + ticketEntity.AssigneeEntity.LastName;
            result.created_User = ticketEntity.created_User;
            result.audit_User = ticketEntity.audit_User;
            return result;
        }
    }
}
