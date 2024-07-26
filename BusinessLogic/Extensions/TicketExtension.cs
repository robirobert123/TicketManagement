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
                PriorityID = dataAccess.PriorityID,
                CategoryEntity = dataAccess.Category.ToBusinessEntity(),
                StatusID = dataAccess.StatusID,
                created_Date = dataAccess.created_Date,
                audit_Date = dataAccess.audit_Date,
                Assignee = dataAccess.Assignee,
                Deleted = dataAccess.Deleted,
                created_User = dataAccess.created_User,
                audit_User = dataAccess.audit_User,
                Images = dataAccess.Images.ToBusinessEntity()
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
                PriorityID = businessEntity.PriorityID,
                Category = businessEntity.CategoryEntity.ToDataAccessEntity(),
                StatusID = businessEntity.StatusID,
                created_Date = businessEntity.created_Date,
                audit_Date = businessEntity.audit_Date,
                Assignee = businessEntity.Assignee,
                Deleted = businessEntity.Deleted,
                created_User = businessEntity.created_User,
                audit_User = businessEntity.audit_User,
                Images = businessEntity.Images.ToDataAccessEntity()
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
