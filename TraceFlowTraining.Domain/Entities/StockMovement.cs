using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TraceFlowTraining.Domain.Enums;

namespace TraceFlowTraining.Domain.Entities;

[Table("TB_STOCK_MOVEMENTS")]
public class StockMovement
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("PRODUCT_ID")]
    public Guid ProductId { get; set; }

    [ForeignKey("PRODUCT_ID")]
    public Product Product { get; set; } = null!;
    
    [Required]
    [Column("QUANTITY")]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    
    [Required]
    [Column("TYPE")]
    public StockMovementType Type { get; set; }
    
    [Required]
    [Column("TIMESTAMP")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}