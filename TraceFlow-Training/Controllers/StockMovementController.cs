using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TraceFlowTraining.Application.DTOs.StockMovement;
using TraceFlowTraining.Domain.Entities;
using TraceFlowTraining.Domain.Interfaces;
using TraceFlowTraining.Domain.Enums;

namespace TraceFlow_Training.Controllers;

/// <summary>
/// Stock movement management endpoints
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StockMovementController : ControllerBase
{
    private readonly IStockMovementRepository _repository;

    private readonly IProductRepository _productRepository;

    public StockMovementController(
        IStockMovementRepository repository,
        IProductRepository productRepository)
    {
        _repository = repository;

        _productRepository = productRepository;
    }

    /// <summary>
    /// Returns all stock movements
    /// </summary>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all stock movements",
        Description = "Returns all stock movement records")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var stockMovements = await _repository.GetAllAsync();

        var response = stockMovements.Select(stock => new StockMovementResponseDto
        {
            Id = stock.Id,
            ProductId = stock.ProductId,
            ProductName = stock.Product?.Name ?? string.Empty,
            Quantity = stock.Quantity,
            Type = (int)stock.Type,
            Timestamp = stock.Timestamp
        });

        return Ok(response);
    }

    /// <summary>
    /// Returns stock movement by id
    /// </summary>
    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get stock movement by id",
        Description = "Returns a stock movement by identifier")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var stock = await _repository.GetByIdAsync(id);

        if (stock is null)
            return NotFound(new
            {
                message = "Stock movement not found"
            });

        var response = new StockMovementResponseDto
        {
            Id = stock.Id,
            ProductId = stock.ProductId,
            ProductName = stock.Product?.Name ?? string.Empty,
            Quantity = stock.Quantity,
            Type = (int)stock.Type,
            Timestamp = stock.Timestamp
        };

        return Ok(response);
    }

    /// <summary>
    /// Creates a new stock movement
    /// </summary>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create stock movement",
        Description = "Creates a new stock movement record")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateStockMovementDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = await _productRepository.GetByIdAsync(dto.ProductId);

        if (product is null)
            return BadRequest(new
            {
                message = "Product not found"
            });

        var stockMovement = new StockMovement
        {
            Id = Guid.NewGuid(),
            ProductId = dto.ProductId,
            Quantity = dto.Quantity,
            Type = (StockMovementType)dto.Type,
            Timestamp = DateTime.UtcNow
        };

        await _repository.CreateAsync(stockMovement);

        return CreatedAtAction(
            nameof(GetById),
            new { id = stockMovement.Id },
            stockMovement);
    }

    /// <summary>
    /// Deletes a stock movement
    /// </summary>
    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Delete stock movement",
        Description = "Deletes a stock movement record")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var stock = await _repository.GetByIdAsync(id);

        if (stock is null)
            return NotFound(new
            {
                message = "Stock movement not found"
            });

        await _repository.DeleteAsync(id);

        return NoContent();
    }
}