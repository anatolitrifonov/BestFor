using BestFor.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    public abstract class EntityBase: IEntityBase, IObjectState
    {
        #region IEntityBase implementation
        public virtual int Id { get; set; } = 0;

        [Required]
        public virtual DateTime DateAdded { get; set; } = DateTime.Now;
        #endregion

        #region IObjectState implementation
        [NotMapped]
        public virtual ObjectState ObjectState { get; set; } = ObjectState.Added;
        #endregion
    }
}
