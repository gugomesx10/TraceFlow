namespace TraceFlowTraining.Application.DTOs.StockMovement;

public class StockMovementResponseDto
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public int Type { get; set; }

    public DateTime Timestamp { get; set; }
}