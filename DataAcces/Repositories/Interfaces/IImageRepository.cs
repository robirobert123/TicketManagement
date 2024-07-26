using DataAcces.GenericRepository;
using System.Collections.Generic;

namespace DataAcces.Repositories.Interfaces
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        IEnumerable<Image> GetAllImages();
        Image GetImageById(int imageId);
        void InsertImage(Image image);
        void DeleteImage(int imageID);
        void UpdateImage(Image image);
        void DeleteAll();
    }
}
