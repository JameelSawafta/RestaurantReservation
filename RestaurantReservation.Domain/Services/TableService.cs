using AutoMapper;
using RestaurantReservation.Domain.Entities;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Table;

namespace RestaurantReservation.Domain.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IMapper _mapper;

    public TableService(ITableRepository tableRepository, IMapper mapper)
    {
        _tableRepository = tableRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<TableDto>> GetAllTablesAsync(int pageNumber, int pageSize, string baseUrl)
    {
        var (tables, totalItemCount) = await _tableRepository.GetAllAsync(pageNumber, pageSize);

        string GeneratePageLink(int page) => $"{baseUrl}?pageNumber={page}&pageSize={pageSize}";

        var pageData = new PageData(totalItemCount, pageSize, pageNumber, GeneratePageLink);
        var tableDtos = _mapper.Map<IEnumerable<TableDto>>(tables);

        return new PaginatedList<TableDto>(tableDtos.ToList(), pageData);
    }

    public async Task<TableDto> GetTableByIdAsync(Guid tableId)
    {
        var table = await _tableRepository.GetByIdAsync(tableId);
        return table == null ? null : _mapper.Map<TableDto>(table);
    }

    public async Task<TableDto> CreateTableAsync(CreateTableDto tableDto)
    {
        var table = _mapper.Map<Table>(tableDto);
        var createdTable = await _tableRepository.CreateAsync(table);
        return _mapper.Map<TableDto>(createdTable);
    }

    public async Task<TableDto> UpdateTableAsync(Guid tableId, UpdateTableDto tableDto)
    {
        var table = await _tableRepository.GetByIdAsync(tableId);
        if (table == null) return null;

        _mapper.Map(tableDto, table);
        var updatedTable = await _tableRepository.UpdateAsync(table);
        return _mapper.Map<TableDto>(updatedTable);
    }

    public async Task<bool> DeleteTableAsync(Guid tableId)
    {
        return await _tableRepository.DeleteAsync(tableId);
    }
}