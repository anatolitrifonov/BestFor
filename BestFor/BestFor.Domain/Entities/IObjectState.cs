using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
