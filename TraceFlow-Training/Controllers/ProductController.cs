using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TraceFlowTraining.Application.DTOs.Product;
using TraceFlowTraining.Domain.Entities;
using TraceFlowTraining.Domain.Interfaces;


namespace TraceFlow_Training.Controllers;

/// <summary>
/// Product management endpoints
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Returns all registered products
    /// </summary>
    /// <returns>List of products</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all products",
        Description = "Returns all registered products from the database")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await _repository.GetAllAsync();

        var response = products.Select(product => new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Quantity = product.Quantity,
            Price = product.Price,
            CreatedAt = product.CreatedAt
        });

        return Ok(response);
    }

    /// <summary>
    /// Returns a product by identifier
    /// </summary>
    /// <param name="id">Product identifier</param>
    /// <returns>Product data</returns>
    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get product by id",
        Description = "Returns a specific product by identifier")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            return NotFound(new
            {
                message = "Product not found"
            });

        var response = new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Sku = product.Sku,
            Quantity = product.Quantity,
            Price = product.Price,
            CreatedAt = product.CreatedAt
        };

        return Ok(response);
    }

    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="dto">Product creation data</param>
    /// <returns>Created product</returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create product",
        Description = "Creates a new product in the database")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Sku = dto.Sku,
            Quantity = dto.Quantity,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(product);

        return CreatedAtAction(
            nameof(GetById),
            new { id = product.Id },
            product);
    }

    /// <summary>
    /// Updates an existing product
    /// </summary>
    /// <param name="id">Product identifier</param>
    /// <param name="dto">Updated product data</param>
    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update product",
        Description = "Updates an existing product")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateProductDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            return NotFound(new
            {
                message = "Product not found"
            });

        product.Name = dto.Name;
        product.Sku = dto.Sku;
        product.Quantity = dto.Quantity;
        product.Price = dto.Price;

        await _repository.UpdateAsync(product);

        return NoContent();
    }

    /// <summary>
    /// Deletes a product
    /// </summary>
    /// <param name="id">Product identifier</param>
    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
        Summary = "Delete product",
        Description = "Deletes a product from the database")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            return NotFound(new
            {
                message = "Product not found"
            });

        await _repository.DeleteAsync(id);

        return NoContent();
    }
}