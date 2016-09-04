using System.Threading;
using System.Threading.Tasks;
using BestFor.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BestFor.Data
{
    public interface IDataContext
    {

        DbSet<TEntity> EntitySet<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
    }
}
