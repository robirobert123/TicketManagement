using BusinessLogic.Entities;
using DataAcces;
using System.Collections.Generic;

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
        internal static ICollection<UserEntity> ToBusinessEntity(this ICollection<AspNetUser> dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            var result = new List<UserEntity>();
            foreach (var item in dataAccess)
            {
                result.Add(item.ToBusinessEntity());
            }

            return result;
        }
        internal static ICollection<AspNetUser> ToDataAccessEntity(this ICollection<UserEntity> businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            var result = new List<AspNetUser>();
            foreach (var item in businessEntity)
            {
                result.Add(item.ToDataAccessEntity());
            }

            return result;
        }
    }
}
