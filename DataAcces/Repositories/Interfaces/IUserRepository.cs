using DataAcces.GenericRepository;
using System.Collections.Generic;

namespace DataAcces.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<AspNetUser>
    {
        IEnumerable<AspNetUser> GetAllUsers();
        AspNetUser GetUserById(string id);
        void InsertUser(AspNetUser user);
        void UpdateUser(AspNetUser user);
        void DeleteUser(string id);
        void DeleteAll();
        AspNetUser GetUserByEmail(string email);
    }
}
