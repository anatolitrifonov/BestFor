using System.Collections.Generic;
using BestFor.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace BestFor.Data
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity GetById(int id);

        IEnumerable<TEntity> List();

        IQueryable<TEntity> Queryable();


        int Count();

        void Insert(TEntity entity);

        void Update(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

    }
}
