using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.OrderItem;

namespace RestaurantReservation.Domain.Services;

public class OrderItemService : CRUDService<OrderItem,OrderItemDto,CreateAndUpdateOrderItemDto>, IOrderItemService
{
    public OrderItemService(ICRUDRepository<OrderItem> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}