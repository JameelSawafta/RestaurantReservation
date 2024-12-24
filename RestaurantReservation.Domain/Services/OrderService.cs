using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Order;

namespace RestaurantReservation.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OrderDto>> GetAllOrdersAsync(int pageNumber, int pageSize, string baseUrl)
    {
        var (orders, totalItemCount) = await _orderRepository.GetAllAsync(pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

        return new PaginatedList<OrderDto>(orderDtos.ToList(), pageData);
    }

    public async Task<OrderDto> GetOrderByIdAsync(Guid orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        return order == null ? null : _mapper.Map<OrderDto>(order);
    }

    public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        var createdOrder = await _orderRepository.CreateAsync(order);
        return _mapper.Map<OrderDto>(createdOrder);
    }

    public async Task<OrderDto> UpdateOrderAsync(Guid orderId, OrderDto orderDto)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null) return null;

        _mapper.Map(orderDto, order);
        var updatedOrder = await _orderRepository.UpdateAsync(order);
        return _mapper.Map<OrderDto>(updatedOrder);
    }

    public async Task<bool> DeleteOrderAsync(Guid orderId)
    {
        return await _orderRepository.DeleteAsync(orderId);
    }
}