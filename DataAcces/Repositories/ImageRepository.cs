using DataAcces.GenericRepository;
using DataAcces.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAcces.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly TicketManagementEntities _context;

        public ImageRepository(TicketManagementEntities context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Image> GetAllImages()
        {
            return _context.Images.ToList();
        }

        public Image GetImageById(int id)
        {
            return _context.Images.Find(id);
        }

        public void InsertImage(Image image)
        {
            _context.Images.Add(image);
        }

        public void UpdateImage(Image image)
        {
            _context.Entry(image).State = System.Data.Entity.EntityState.Modified;
        }
        public void DeleteImage(int id)
        {
            Image image = _context.Images.Find(id);
            _context.Images.Remove(image);
        }
        public void DeleteAll()
        {
            _context.Images.RemoveRange(_context.Images);
        }
    }
}

