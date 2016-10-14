using System;

namespace BestFor.Domain.Interfaces
{
    /// <summary>
    /// Helps all domain entries to have a common set of properties
    /// </summary>
    public interface IEntityBase
    {
        int Id { get; set; }

        DateTime DateAdded { get; set; }
    }
}
