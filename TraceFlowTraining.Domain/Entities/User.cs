using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraceFlowTraining.Domain.Entities;

[Table("TB_USERS")]
public class User
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    [Required]
    [Column("USERNAME")]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [Column("PASSWORD")]
    [StringLength(255)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Column("ROLE")]
    [StringLength(20)]
    public string Role { get; set; } = "User";
}