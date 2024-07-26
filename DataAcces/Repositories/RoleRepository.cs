using DataAcces.GenericRepository;
using DataAcces.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAcces.Repositories
{
    public class RoleRepository : GenericRepository<AspNetRole>, IRoleRepository
    {
        private readonly TicketManagementEntities _context;

        public RoleRepository(TicketManagementEntities context) : base(context)
        {
            _context = context;
        }
        IEnumerable<AspNetRole> IRoleRepository.GetAllRoles()
        {
            return _context.AspNetRoles.ToList();
        }
        public AspNetRole GetRoleById(string id)
        {
            return _context.AspNetRoles.Find(id);
        }
        public void InsertRole(AspNetRole role)
        {
            _context.AspNetRoles.Add(role);
        }
        public void UpdateRole(AspNetRole role)
        {
            _context.Entry(role).State = System.Data.Entity.EntityState.Modified;
        }
        public void DeleteRole(string id)
        {
            AspNetRole role = _context.AspNetRoles.Find(id);
            _context.AspNetRoles.Remove(role);
        }
        public void DeleteAll()
        {
            _context.AspNetRoles.RemoveRange(_context.AspNetRoles);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
