using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Table;

namespace RestaurantReservation.Domain.Services;

public class TableService : CRUDService<Table,TableDto,CreateAndUpdateTableDto>,  ITableService
{
    public TableService(ICRUDRepository<Table> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}