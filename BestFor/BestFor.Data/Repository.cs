using BestFor.Domain;
using BestFor.Domain.Entities;
// This is needed for DbSet. We would need to wrap the dbset with something in order to avoid this reference
// and not depend on ef in this class
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BestFor.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity :EntityBase // class, IObjectState
    {
        protected readonly IDataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(IDataContext context) //, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            //_unitOfWork = unitOfWork;
            _dbSet = context.EntitySet<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return null;
        }

        public virtual IEnumerable<TEntity> List()
        {
            return _dbSet;
        }

        public virtual IQueryable<TEntity> Active()
        {
            return _dbSet;
        }

        public virtual IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }

        public virtual int Count()
        {
            return _dbSet.Count();
        }

        public virtual void Insert(TEntity entity)
        {
            // This whole business about object state is decaoupling domain and data
            // Domain should not know anything about ef. All ef is in Data
            // Still interesting if it is repository job to deal with object states or context
            // Grand idea is that it is context's job because context deals objects from different sets.
            // Not in our case yet but we will see.
            entity.ObjectState = ObjectState.Added;
            var er = _dbSet.Add(entity);
            // var er = _dbSet.Attach(entity); // Does not work in .Net Core.
            _context.SyncObjectState(entity);
        }

        public virtual void Update(TEntity entity)
        {
            // See comments in insert
            entity.ObjectState = ObjectState.Modified;
            _dbSet.Update(entity);
            // _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
