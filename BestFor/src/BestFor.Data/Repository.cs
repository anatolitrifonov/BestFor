using System.Linq;
using System.Collections.Generic;
using BestFor.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace BestFor.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity :EntityBase // class, IObjectState
    {
        private readonly IDataContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(IDataContext context) //, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            //_unitOfWork = unitOfWork;
            _dbSet = context.EntitySet<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return null;
        }

        public IEnumerable<TEntity> List()
        {
            return _dbSet;
        }

        public virtual void Insert(TEntity entity)
        {
            //entity.ObjectState = ObjectState.Added;
            _dbSet.Attach(entity);
            //_context.SyncObjectState(entity);
        }

    }
}
