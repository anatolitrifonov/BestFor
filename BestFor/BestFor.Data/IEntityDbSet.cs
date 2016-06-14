using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Data
{
    public interface IEntityDbSet<TEntity> : IEnumerable<TEntity>
    {
    }
}
