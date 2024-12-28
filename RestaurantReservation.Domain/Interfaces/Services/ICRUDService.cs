using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface ICRUDService<TDto,CreateAndUpdateTDto>
{
    Task<PaginatedList<TDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<TDto> GetByIdAsync(Guid id);
    Task<TDto> CreateAsync(CreateAndUpdateTDto createDto);
    Task<TDto> UpdateAsync(Guid id, CreateAndUpdateTDto updateDto);
    Task<bool> DeleteAsync(Guid id);
}