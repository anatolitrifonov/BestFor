using System.Collections.Generic;
using BestFor.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace BestFor.Data
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity GetById(int id);

        IEnumerable<TEntity> List();

        void Insert(TEntity entity);

        void Update(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

    }
}
