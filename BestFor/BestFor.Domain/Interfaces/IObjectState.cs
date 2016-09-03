using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Interfaces
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}
