using DataAcces.GenericRepository;
using DataAcces.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAcces.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly TicketManagementEntities _context;

        public CommentRepository(TicketManagementEntities context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }
        public Comment GetCommentById(int id)
        {
            return _context.Comments.Find(id);
        }
        public void InsertComment(Comment comment)
        {
            _context.Comments.Add(comment);
        }
        public void UpdateComment(Comment comment)
        {
            _context.Entry(comment).State = System.Data.Entity.EntityState.Modified;
        }
        public void DeleteComment(int id)
        {
            Comment comment = _context.Comments.Find(id);
            _context.Comments.Remove(comment);
        }
        public void DeleteAll()
        {
            _context.Comments.RemoveRange(_context.Comments);
        }
    }
}
