using BusinessLogic.Entities;
using DataAcces;
using System.Collections.Generic;

namespace BusinessLogic.Extensions
{
    internal static class ImageExtension
    {
        internal static ImageEntity ToBusinessEntity(this Image dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            return new ImageEntity
            {
                ImageID = dataAccess.ImageID,
                Byte = dataAccess.BinaryCode,
                TicketID = dataAccess.TicketID
            };
        }
        internal static Image ToDataAccessEntity(this ImageEntity businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            return new Image
            {
                ImageID = businessEntity.ImageID,
                BinaryCode = businessEntity.Byte,
                TicketID = businessEntity.TicketID
            };
        }

        internal static ICollection<ImageEntity> ToBusinessEntity(this ICollection<Image> dataAccess)
        {
            if (dataAccess == null)
            {
                return null;
            }

            var result = new List<ImageEntity>();
            foreach (var item in dataAccess)
            {
                result.Add(item.ToBusinessEntity());
            }

            return result;
        }
        internal static ICollection<Image> ToDataAccessEntity(this ICollection<ImageEntity> businessEntity)
        {
            if (businessEntity == null)
            {
                return null;
            }

            var result = new List<Image>();
            foreach (var item in businessEntity)
            {
                result.Add(item.ToDataAccessEntity());
            }

            return result;
        }
    }
}
