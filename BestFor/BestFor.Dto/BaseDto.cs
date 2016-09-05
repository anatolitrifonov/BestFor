using System;

namespace BestFor.Dto
{
    /// <summary>
    /// Used to ensure that DtoConvertable interface returns dto objects from this library
    /// </summary>
    public abstract class BaseDto
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
