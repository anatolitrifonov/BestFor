using System;
using BestFor.Dto;

namespace BestFor.Domain.Interfaces
{
    public interface IDtoConvertable<T> where T : BaseDto
    {
        T ToDto();

        ObjectsIdentifier FromDto(T dto);
    }
}
