namespace Stopify.Domain.Contracts.Common;

public interface IDto<T, TDto> where T : class where TDto : class
{
    TDto MapToDto(T entity);
}
