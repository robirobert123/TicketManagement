using BusinessLogic.Entities;
using DataAcces;

namespace BusinessLogic.Extensions
{
    internal static class UserExtension
    {
        internal static UserEntity ToBusinessEntity(this AspNetUser dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            return new UserEntity
            {
                Id = dataAccess.Id,
                FirstName = dataAccess.FirstName,
                LastName = dataAccess.LastName,
                UserName = dataAccess.UserName
            };
        }
        internal static AspNetUser ToDataAccessEntity(this UserEntity businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }
            return new AspNetUser
            {
                Id = businessEntity.Id,
                FirstName = businessEntity.FirstName,
                LastName = businessEntity.LastName,
                UserName = businessEntity.UserName
            };
        }
    }
}
