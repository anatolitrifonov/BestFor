using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;

namespace BestFor.Domain.Entities
{
    public abstract class BaseDomainObject
    {
 //       [Key]
        public ObjectsIdentifier Id { get; set; }

        public BaseDomainObject()
        {
            Id = new ObjectsIdentifier();
        }
    }
}
