using System;

namespace DataAcces.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IImageRepository ImageRepository { get; }
        IPriorityRepository PriorityRepository { get; }
        IRoleRepository RoleRepository { get; }
        IStatusRepository StatusRepository { get; }
        ITicketRepository TicketRepository { get; }

        void Save();
    }
}
