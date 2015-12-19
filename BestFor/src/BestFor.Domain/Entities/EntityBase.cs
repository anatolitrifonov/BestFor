using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    public abstract class EntityBase: IObjectState
    {
        public virtual int Id { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; } = ObjectState.Added;
    }
}
