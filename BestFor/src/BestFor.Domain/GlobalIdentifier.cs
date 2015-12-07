using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Domain
{
    public class ObjectsIdentifier
    {
        private Guid _id;
        public ObjectsIdentifier()
        {
            _id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return _id.ToString();
        }
    }
}
