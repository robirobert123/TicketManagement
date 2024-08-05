using BusinessLogic.Entities;
using DataAcces;
using System.Collections.Generic;

namespace BusinessLogic.Extensions
{
    internal static class PriorityExtension
    {
        internal static PriorityEntity ToBusinessEntity(this Priority dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            return new PriorityEntity
            {
                PriorityID = dataAccess.PriorityID,
                PriorityName = dataAccess.Name
            };
        }
        internal static Priority ToDataAccessEntity(this PriorityEntity businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }
            return new Priority
            {
                PriorityID = businessEntity.PriorityID,
                Name = businessEntity.PriorityName
            };
        }
        internal static ICollection<PriorityEntity> ToBusinessEntity(this ICollection<Priority> dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            var result = new List<PriorityEntity>();
            foreach (var item in dataAccess)
            {
                result.Add(item.ToBusinessEntity());
            }

            return result;
        }
        internal static ICollection<Priority> ToDataAccessEntity(this ICollection<PriorityEntity> businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            var result = new List<Priority>();
            foreach (var item in businessEntity)
            {
                result.Add(item.ToDataAccessEntity());
            }

            return result;
        }

    }
}
