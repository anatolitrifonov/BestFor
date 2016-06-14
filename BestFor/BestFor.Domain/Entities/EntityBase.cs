using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    public abstract class EntityBase: IObjectState
    {
        public virtual int Id { get; set; }

        [Required]
        public virtual DateTime DateAdded { get; set; } = DateTime.Now;

        [NotMapped]
        public ObjectState ObjectState { get; set; } = ObjectState.Added;
    }
}
