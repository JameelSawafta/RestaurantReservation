using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Table;

namespace RestaurantReservation.Domain.Interfaces.Services;

public interface ITableService : ICRUDService<TableDto,CreateAndUpdateTableDto>
{
}