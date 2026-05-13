namespace TraceFlowTraining.Application.DTOs.Product;

public class ProductResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }
}