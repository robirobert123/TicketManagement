using BusinessLogic.Entities;
using DataAcces;
using System.Collections.Generic;

namespace BusinessLogic.Extensions
{
    internal static class CategoryExtension
    {
        internal static CategoryEntity ToBusinessEntity(this Category dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            return new CategoryEntity
            {
                CategoryID = dataAccess.CategoryID,
                CategoryName = dataAccess.Name
            };
        }
        internal static Category ToDataAccessEntity(this CategoryEntity businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }
            return new Category
            {
                CategoryID = businessEntity.CategoryID,
                Name = businessEntity.CategoryName
            };
        }
        internal static ICollection<CategoryEntity> ToBusinessEntity(this ICollection<Category> dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            var result = new List<CategoryEntity>();
            foreach (var item in dataAccess)
            {
                result.Add(item.ToBusinessEntity());
            }

            return result;
        }
        internal static ICollection<Category> ToDataAccessEntity(this ICollection<CategoryEntity> businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            var result = new List<Category>();
            foreach (var item in businessEntity)
            {
                result.Add(item.ToDataAccessEntity());
            }

            return result;
        }
    }
}
