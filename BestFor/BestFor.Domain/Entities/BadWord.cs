using BestFor.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestFor.Domain.Entities
{
    public class BadWord : EntityBase, IFirstIndex
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [Required]
        public string Phrase { get; set; }

        #region IFirstIndex implementation
        [NotMapped]
        public string IndexKey { get { return Phrase; } }
        #endregion
    }
}
