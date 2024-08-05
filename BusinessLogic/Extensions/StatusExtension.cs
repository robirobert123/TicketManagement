using BusinessLogic.Entities;
using DataAcces;
using System.Collections.Generic;

namespace BusinessLogic.Extensions
{
    internal static class StatusExtension
    {
        internal static StatusEntity ToBusinessEntity(this Status dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            return new StatusEntity
            {
                StatusID = dataAccess.StatusID,
                StatusName = dataAccess.Name
            };
        }

        internal static Status ToDataAccessEntity(this StatusEntity businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }
            return new Status
            {
                StatusID = businessEntity.StatusID,
                Name = businessEntity.StatusName
            };
        }
        internal static ICollection<StatusEntity> ToBusinessEntity(this ICollection<Status> dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            var result = new List<StatusEntity>();
            foreach (var item in dataAccess)
            {
                result.Add(item.ToBusinessEntity());
            }

            return result;
        }
        internal static ICollection<Status> ToDataAccessEntity(this ICollection<StatusEntity> businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            var result = new List<Status>();
            foreach (var item in businessEntity)
            {
                result.Add(item.ToDataAccessEntity());
            }

            return result;
        }
    }
}
