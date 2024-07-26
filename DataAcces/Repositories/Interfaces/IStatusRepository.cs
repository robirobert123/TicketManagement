using DataAcces.GenericRepository;
using System.Collections.Generic;
namespace DataAcces.Repositories.Interfaces
{
    public interface IStatusRepository : IGenericRepository<Status>
    {
        IEnumerable<Status> GetAllStatus();
        Status GetStatusById(int id);
        void InsertStatus(Status status);
        void UpdateStatus(Status status);
        void DeleteStatus(int id);
        void DeleteAll();
    }
}
