using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;

namespace RestaurantReservation.Domain.Services;

public class OrderService : CRUDService<Order,OrderDto,CreateAndUpdateOrderDto>,  IOrderService
{
    public OrderService(ICRUDRepository<Order> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}