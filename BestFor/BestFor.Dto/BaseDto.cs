using System;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Dto
{
    /// <summary>
    /// Used to ensure that DtoConvertable interface returns dto objects from this library
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class BaseDto
    {
        public int Id { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
