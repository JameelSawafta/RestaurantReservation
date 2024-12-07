using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Models.Table;

namespace RestaurantReservation.API.Controllers;

[ApiController]
[Route("api/table")]
[ApiVersion(1.0)]
public class TablesController : Controller
{
    private readonly ITableService _tableService;

    public TablesController(ITableService tableService)
    {
        _tableService = tableService;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<TableDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1 || pageSize < 1)
            return BadRequest("PageNumber and PageSize must be greater than 0.");

        string baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";

        var paginatedTables = await _tableService.GetAllTablesAsync(pageNumber, pageSize, baseUrl);
        return Ok(paginatedTables);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TableDto>> GetById(Guid id)
    {
        var table = await _tableService.GetTableByIdAsync(id);
        if (table == null) return NotFound();
        return Ok(table);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTableDto tableDto)
    {
        var createdTable = await _tableService.CreateTableAsync(tableDto);
        return CreatedAtAction(nameof(GetById), new { id = createdTable.TableId }, createdTable);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TableDto>> Update(Guid id, UpdateTableDto tableDto)
    {
        var updatedTable = await _tableService.UpdateTableAsync(id, tableDto);
        if (updatedTable == null) return NotFound();
        return Ok(updatedTable);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _tableService.DeleteTableAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}