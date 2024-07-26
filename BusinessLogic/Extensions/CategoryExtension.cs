using BusinessLogic.Entities;
using DataAcces;

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
    }
}
