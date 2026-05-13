using System.ComponentModel.DataAnnotations;

namespace TraceFlowTraining.Application.DTOs.StockMovement;

public class CreateStockMovementDto
{
    [Required]
    public Guid ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Required]
    public int Type { get; set; }
}