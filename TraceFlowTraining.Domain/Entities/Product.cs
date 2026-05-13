using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraceFlowTraining.Domain.Entities;

[Table("TB_PRODUCTS")]
public class Product
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    [Required]
    [Column("NAME")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Column("SKU")]
    [StringLength(50)]
    public string SKU { get; set; } = string.Empty;
    
    [Required]
    [Column("QUANTITY")]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
    
    [Required]
    [Column("PRICE", TypeName = "decimal(18, 2)")]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    
    [Required]
    [Column("CREATED_AT")]
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    
    // navigation
    public ICollection<StockMovement> StockMovements { get; set; }
        = new List<StockMovement>();

}