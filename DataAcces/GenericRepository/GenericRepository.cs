using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAcces.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal TicketManagementEntities context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(TicketManagementEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
        public virtual TEntity Get(int id)
        {
            return dbSet.Find(id);
        }
        public virtual void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            dbSet.Remove(entityToDelete);
        }
    }
}
