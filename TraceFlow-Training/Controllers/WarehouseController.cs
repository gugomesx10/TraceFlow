using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TraceFlowTraining.Application.DTOs.Warehouse;
using TraceFlowTraining.Domain.Entities;
using TraceFlowTraining.Domain.Interfaces;

namespace TraceFlow_Training.Controllers;

/// <summary>
/// Warehouse management endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseRepository _repository;

    public WarehouseController(IWarehouseRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Returns all registered warehouses
    /// </summary>
    /// <returns>List of warehouses</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all warehouses",
        Description = "Returns all registered warehouses from the database")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var warehouses = await _repository.GetAllAsync();

        var response = warehouses.Select(warehouse => new WarehouseResponseDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity
        });

        return Ok(response);
    }

    /// <summary>
    /// Returns a warehouse by identifier
    /// </summary>
    /// <param name="id">Warehouse identifier</param>
    /// <returns>Warehouse data</returns>
    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get warehouse by id",
        Description = "Returns a specific warehouse by identifier")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var warehouse = await _repository.GetByIdAsync(id);

        if (warehouse is null)
            return NotFound(new
            {
                message = "Warehouse not found"
            });

        var response = new WarehouseResponseDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            Location = warehouse.Location,
            Capacity = warehouse.Capacity
        };

        return Ok(response);
    }

    /// <summary>
    /// Creates a new warehouse
    /// </summary>
    /// <param name="dto">Warehouse creation data</param>
    /// <returns>Created warehouse</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create warehouse",
        Description = "Creates a new warehouse in the database")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateWarehouseDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var warehouse = new Warehouse
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Location = dto.Location,
            Capacity = dto.Capacity
        };

        await _repository.CreateAsync(warehouse);

        return CreatedAtAction(
            nameof(GetById),
            new { id = warehouse.Id },
            warehouse);
    }

    /// <summary>
    /// Updates an existing warehouse
    /// </summary>
    /// <param name="id">Warehouse identifier</param>
    /// <param name="dto">Updated warehouse data</param>
    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update warehouse",
        Description = "Updates an existing warehouse")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateWarehouseDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var warehouse = await _repository.GetByIdAsync(id);

        if (warehouse is null)
            return NotFound(new
            {
                message = "Warehouse not found"
            });

        warehouse.Name = dto.Name;
        warehouse.Location = dto.Location;
        warehouse.Capacity = dto.Capacity;

        await _repository.UpdateAsync(warehouse);

        return NoContent();
    }

    /// <summary>
    /// Deletes a warehouse
    /// </summary>
    /// <param name="id">Warehouse identifier</param>
    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Delete warehouse",
        Description = "Deletes a warehouse from the database")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var warehouse = await _repository.GetByIdAsync(id);

        if (warehouse is null)
            return NotFound(new
            {
                message = "Warehouse not found"
            });

        await _repository.DeleteAsync(id);

        return NoContent();
    }
}