using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;

namespace BestFor.Fakes
{
    public class FakeInternalEntityEntry<TEntity> : InternalEntityEntry
    {
        private TEntity _entity;

        public FakeInternalEntityEntry(TEntity entity) : base(
            new Mock<IStateManager>().Object,
            new Mock<IEntityType>().Object)
            // ,            new Mock<IEntityEntryMetadataServices>().Object
        {
            _entity = entity;
        }

        public override object Entity { get { return _entity; } }

    }
}
