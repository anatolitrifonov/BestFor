using System;
using BestFor.Dto;
using BestFor.Domain.Entities;

namespace BestFor.Domain.Interfaces
{
    public interface IDtoConvertable<T> where T : BaseDto
    {
        T ToDto();

        int FromDto(T dto);
    }
}
