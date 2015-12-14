using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace BestFor.Data
{
    public interface IDataContext
    {

        DbSet<TEntity> EntitySet<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsync();
    }
}
