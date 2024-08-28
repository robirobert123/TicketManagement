using BusinessLogic.Entities;
using DataAcces;
using System.Collections.Generic;

namespace BusinessLogic.Extensions
{
    internal static class CommentExtension
    {
        internal static CommentEntity ToBusinessEntity(this Comment dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            return new CommentEntity
            {
                CommentID = dataAccess.CommentID,
                CommentText = dataAccess.CommentText,
                CommentDate = dataAccess.PostedAt,
                TicketID = dataAccess.TicketID
            };
        }
        internal static Comment ToDataAccessEntity(this CommentEntity businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            return new Comment
            {
                CommentID = businessEntity.CommentID,
                CommentText = businessEntity.CommentText,
                PostedAt = businessEntity.CommentDate,
                TicketID = businessEntity.TicketID
            };
        }
        internal static ICollection<CommentEntity> ToBusinessEntity(this ICollection<Comment> dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            var result = new List<CommentEntity>();
            foreach (var item in dataAccess)
            {
                result.Add(item.ToBusinessEntity());
            }
            return result;
        }
        internal static ICollection<Comment> ToDataAccessEntity(this ICollection<CommentEntity> businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            var result = new List<Comment>();
            foreach (var item in businessEntity)
            {
                result.Add(item.ToDataAccessEntity());
            }
            return result;
        }
    }
}
