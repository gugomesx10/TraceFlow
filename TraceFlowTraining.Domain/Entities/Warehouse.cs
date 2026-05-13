using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraceFlowTraining.Domain.Entities;

[Table("TB_WAREHOUSE")]
public class Warehouse
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }
    
    [Required]
    [Column("NAME")]
    [StringLength(100)]
    public string Name { get; set; } =  String.Empty;
    
    [Required]
    [Column("LOCATION")]
    [StringLength(200)]
    public string Location { get; set; } =  String.Empty;
    
    [Required]
    [Column("CAPACITY")]
    [Range(1, int.MaxValue)]
    public int Capacity { get; set; } 
}