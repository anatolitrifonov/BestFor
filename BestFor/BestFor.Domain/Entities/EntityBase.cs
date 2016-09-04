using BestFor.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    public abstract class EntityBase: IObjectState
    {
        public virtual int Id { get; set; } = 0;

        [Required]
        public virtual DateTime DateAdded { get; set; } = DateTime.Now;

        #region IObjectState implementation
        [NotMapped]
        public ObjectState ObjectState { get; set; } = ObjectState.Added;
        #endregion

    }
}
