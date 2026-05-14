namespace TraceFlowTraining.Application.DTOs.Warehouse;

public class WarehouseResponseDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public int Capacity { get; set; }
}