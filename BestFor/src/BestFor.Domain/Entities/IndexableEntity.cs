using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Domain.Entities
{
    public abstract class IndexableEntity : EntityBase
    {
        public virtual string IndexKey { get; set; }

    }
}
