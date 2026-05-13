using System.ComponentModel.DataAnnotations;

namespace TraceFlowTraining.Application.DTOs.Product;

public class UpdateProductDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Sku { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}