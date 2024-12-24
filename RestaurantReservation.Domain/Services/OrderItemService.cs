using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.OrderItem;

namespace RestaurantReservation.Domain.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<OrderItemDto>> GetAllOrderItemsAsync(int pageNumber, int pageSize, string baseUrl)
    {
        var (orderItems, totalItemCount) = await _orderItemRepository.GetAllAsync(pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        var orderItemDtos = _mapper.Map<IEnumerable<OrderItemDto>>(orderItems);

        return new PaginatedList<OrderItemDto>(orderItemDtos.ToList(), pageData);
    }

    public async Task<OrderItemDto> GetOrderItemByIdAsync(Guid orderItemId)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(orderItemId);
        return orderItem == null ? null : _mapper.Map<OrderItemDto>(orderItem);
    }

    public async Task<OrderItemDto> CreateOrderItemAsync(OrderItemDto orderItemDto)
    {
        var orderItem = _mapper.Map<OrderItem>(orderItemDto);
        var createdOrderItem = await _orderItemRepository.CreateAsync(orderItem);
        return _mapper.Map<OrderItemDto>(createdOrderItem);
    }

    public async Task<OrderItemDto> UpdateOrderItemAsync(Guid orderItemId, OrderItemDto orderItemDto)
    {
        var orderItem = await _orderItemRepository.GetByIdAsync(orderItemId);
        if (orderItem == null) return null;

        _mapper.Map(orderItemDto, orderItem);
        var updatedOrderItem = await _orderItemRepository.UpdateAsync(orderItem);
        return _mapper.Map<OrderItemDto>(updatedOrderItem);
    }

    public async Task<bool> DeleteOrderItemAsync(Guid orderItemId)
    {
        return await _orderItemRepository.DeleteAsync(orderItemId);
    }
}