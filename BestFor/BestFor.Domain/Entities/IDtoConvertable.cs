using System;
using BestFor.Dto;
using BestFor.Domain.Entities;

namespace BestFor.Domain.Entities
{
    public interface IDtoConvertable<T> where T : BaseDto
    {
        T ToDto();

        int FromDto(T dto);
    }
}
