using BusinessLogic.Entities;
using DataAcces;
using System.Collections.Generic;

namespace BusinessLogic.Extensions
{
    internal static class TicketExtension
    {
        internal static TicketEntity ToBusinessEntity(this Ticket dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            return new TicketEntity
            {
                TicketID = dataAccess.TicketID,
                Title = dataAccess.Title,
                Description = dataAccess.Description,
                PriorityEntity = dataAccess.Priority.ToBusinessEntity(),
                CategoryEntity = dataAccess.Category.ToBusinessEntity(),
                StatusEntity = dataAccess.Status.ToBusinessEntity(),
                created_Date = dataAccess.created_Date,
                audit_Date = dataAccess.audit_Date,
                AssigneeEntity = dataAccess.AspNetUser?.ToBusinessEntity(),
                Deleted = dataAccess.Deleted,
                created_User = dataAccess.created_User,
                audit_User = dataAccess.audit_User,
                Images = dataAccess.Images.ToBusinessEntity(),
                Comments = dataAccess.Comments.ToBusinessEntity()
            };
        }
        internal static Ticket ToDataAccessEntity(this TicketEntity businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            return new Ticket
            {
                TicketID = businessEntity.TicketID,
                Title = businessEntity.Title,
                Description = businessEntity.Description,
                Priority = businessEntity.PriorityEntity.ToDataAccessEntity(),
                Category = businessEntity.CategoryEntity.ToDataAccessEntity(),
                Status = businessEntity.StatusEntity.ToDataAccessEntity(),
                created_Date = businessEntity.created_Date,
                audit_Date = businessEntity.audit_Date,
                AspNetUser = businessEntity.AssigneeEntity?.ToDataAccessEntity(),
                Deleted = businessEntity.Deleted,
                created_User = businessEntity.created_User,
                audit_User = businessEntity.audit_User,
                Images = businessEntity.Images.ToDataAccessEntity(),
                Comments = businessEntity.Comments.ToDataAccessEntity()
            };
        }
        internal static ICollection<TicketEntity> ToBusinessEntity(this ICollection<Ticket> dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            var result = new List<TicketEntity>();
            foreach (var item in dataAccess)
            {
                result.Add(item.ToBusinessEntity());
            }
            return result;
        }
        internal static ICollection<Ticket> ToDataAccessEntity(this ICollection<TicketEntity> businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            var result = new List<Ticket>();
            foreach (var item in businessEntity)
            {
                result.Add(item.ToDataAccessEntity());
            }
            return result;
        }
    }
}
