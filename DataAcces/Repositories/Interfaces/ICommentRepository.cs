using DataAcces.GenericRepository;
using System.Collections.Generic;

namespace DataAcces.Repositories.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        IEnumerable<Comment> GetAllComments();
        Comment GetCommentById(int id);
        void InsertComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int id);
        void DeleteAll();
    }
}
