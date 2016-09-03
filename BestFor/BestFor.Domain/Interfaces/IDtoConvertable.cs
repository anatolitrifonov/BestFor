using BestFor.Dto;

namespace BestFor.Domain.Interfaces
{
    public interface IDtoConvertable<T> where T : BaseDto
    {
        T ToDto();

        int FromDto(T dto);
    }
}
