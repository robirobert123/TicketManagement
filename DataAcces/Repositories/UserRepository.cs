using DataAcces.GenericRepository;
using DataAcces.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAcces.Repositories
{
    public class UserRepository : GenericRepository<AspNetUser>, IUserRepository
    {
        private readonly TicketManagementEntities _context;

        public UserRepository(TicketManagementEntities context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return _context.AspNetUsers.ToList();
        }
        public AspNetUser GetUserById(string id)
        {
            return _context.AspNetUsers.Find(id);
        }
        public void InsertUser(AspNetUser user)
        {
            _context.AspNetUsers.Add(user);
        }
        public void UpdateUser(AspNetUser user)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
        }
        public void DeleteUser(string id)
        {
            AspNetUser user = _context.AspNetUsers.Find(id);
            _context.AspNetUsers.Remove(user);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void DeleteAll()
        {
            _context.AspNetUsers.RemoveRange(_context.AspNetUsers);
        }
    }
}
