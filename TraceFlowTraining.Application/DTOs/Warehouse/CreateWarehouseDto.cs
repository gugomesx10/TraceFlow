using System.ComponentModel.DataAnnotations;

namespace TraceFlowTraining.Application.DTOs.Warehouse;

public class CreateWarehouseDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Location { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }
}