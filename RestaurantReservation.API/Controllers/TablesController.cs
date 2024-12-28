using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Table;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/table")]
[ApiVersion("1.0")]
[Authorize]
public class TablesController : Controller
{
    private readonly ITableService _tableService;

    public TablesController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpGet]
    public async Task<PaginatedList<TableDto>> GetAll(int pageNumber, int pageSize)
    {
        var paginatedTables = await _tableService.GetAllAsync(pageNumber, pageSize);
        return paginatedTables;
    }

    [HttpGet("{id}")]
    public async Task<TableDto> GetById(Guid id)
    {
        var table = await _tableService.GetByIdAsync(id);
        return table;
    }

    [HttpPost]
    public async Task<TableDto> Create(CreateAndUpdateTableDto tableDto)
    {
        var createdTable = await _tableService.CreateAsync(tableDto);
        return createdTable;
    }

    [HttpPut("{id}")]
    public async Task<TableDto> Update(Guid id, CreateAndUpdateTableDto tableDto)
    {
        var updatedTable = await _tableService.UpdateAsync(id, tableDto);
        return updatedTable;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _tableService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}