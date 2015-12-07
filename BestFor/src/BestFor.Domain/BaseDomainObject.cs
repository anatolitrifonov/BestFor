using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Domain
{
    public abstract class BaseDomainObject
    {
        public ObjectsIdentifier Id { get; set; }

        public BaseDomainObject()
        {
            Id = new ObjectsIdentifier();
        }
    }
}
