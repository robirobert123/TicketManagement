using BusinessLogic.Entities;
using DataAcces;

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
    }
}
