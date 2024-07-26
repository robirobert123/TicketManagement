using DataAcces.GenericRepository;
using System.Collections.Generic;

namespace DataAcces.Repositories.Interfaces
{
    public interface IRoleRepository : IGenericRepository<AspNetRole>
    {
        IEnumerable<AspNetRole> GetAllRoles();
        AspNetRole GetRoleById(string roleId);
        void InsertRole(AspNetRole role);
        void DeleteRole(string roleId);
        void UpdateRole(AspNetRole role);
        void DeleteAll();
    }
}
