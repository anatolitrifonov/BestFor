using System;

namespace BestFor.Domain.Entities
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
