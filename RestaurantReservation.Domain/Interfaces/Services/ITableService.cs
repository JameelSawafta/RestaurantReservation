using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Table;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface ITableService
{
    Task<PaginatedList<TableDto>> GetAllTablesAsync(int pageNumber, int pageSize, string baseUrl);
    Task<TableDto> GetTableByIdAsync(Guid tableId);
    Task<TableDto> CreateTableAsync(CreateTableDto tableDto);
    Task<TableDto> UpdateTableAsync(Guid tableId, UpdateTableDto tableDto);
    Task<bool> DeleteTableAsync(Guid tableId);
}