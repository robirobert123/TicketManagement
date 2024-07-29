using BusinessLogic.Entities;
using DataAcces;

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

    }
}
