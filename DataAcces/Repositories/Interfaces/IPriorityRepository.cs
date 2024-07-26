using DataAcces.GenericRepository;
using System.Collections.Generic;

namespace DataAcces.Repositories.Interfaces
{
    public interface IPriorityRepository : IGenericRepository<Priority>
    {
        IEnumerable<Priority> GetAllPriorities();
        Priority GetPriorityById(int priorityId);
        void InsertPriority(Priority priority);
        void DeletePriority(int priorityId);
        void UpdatePriority(Priority priority);
        void DeleteAll();
    }
}
