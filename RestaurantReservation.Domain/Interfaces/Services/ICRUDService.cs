using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface ICRUDService<TDto>
{
    Task<PaginatedList<TDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<TDto> GetByIdAsync(Guid id);
    Task<TDto> CreateAsync(TDto createDto);
    Task<TDto> UpdateAsync(Guid id, TDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}