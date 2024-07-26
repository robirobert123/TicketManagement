using DataAcces.Repositories.Interfaces;

namespace DataAcces.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private TicketManagementEntities _context = new TicketManagementEntities();
        public ITicketRepository ticketRepository;
        public IPriorityRepository priorityRepository;
        public IImageRepository imageRepository;
        public IStatusRepository statusRepository;
        public IRoleRepository roleRepository;
        public IUserRepository userRepository;
        public ICategoryRepository categoryRepository;

        public ITicketRepository TicketRepository
        {
            get
            {
                if (this.ticketRepository == null)
                {
                    this.ticketRepository = new TicketRepository(_context);
                }
                return ticketRepository;
            }
        }
        public IPriorityRepository PriorityRepository
        {
            get
            {
                if (this.priorityRepository == null)
                {
                    this.priorityRepository = new PriorityRepository(_context);
                }
                return priorityRepository;
            }
        }
        public IImageRepository ImageRepository
        {
            get
            {
                if (this.imageRepository == null)
                {
                    this.imageRepository = new ImageRepository(_context);
                }
                return imageRepository;
            }
        }
        public IStatusRepository StatusRepository
        {
            get
            {
                if (this.statusRepository == null)
                {
                    this.statusRepository = new StatusRepository(_context);
                }
                return statusRepository;
            }
        }
        public IRoleRepository RoleRepository
        {
            get
            {
                if (this.roleRepository == null)
                {
                    this.roleRepository = new RoleRepository(_context);
                }
                return roleRepository;
            }
        }
        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(_context);
                }
                return userRepository;
            }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new CategoryRepository(_context);
                }
                return categoryRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
