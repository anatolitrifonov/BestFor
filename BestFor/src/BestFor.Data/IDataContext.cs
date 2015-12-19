using System.Threading;
using System.Threading.Tasks;
using BestFor.Domain.Entities;
using Microsoft.Data.Entity;


namespace BestFor.Data
{
    public interface IDataContext
    {

        DbSet<TEntity> EntitySet<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
    }
}
