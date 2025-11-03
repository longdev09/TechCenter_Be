using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CHUNGCHI")]
[Index("Tenchungchi", Name = "UQ__CHUNGCHI__33E88737B6A595F5", IsUnique = true)]
public partial class Chungchi
{
    [Key]
    [Column("ID_CHUNGCHI")]
    public int IdChungchi { get; set; }

    [Column("TENCHUNGCHI")]
    [StringLength(200)]
    public string Tenchungchi { get; set; } = null!;

    [Column("MOTACC")]
    public string? Motacc { get; set; }

    [Column("LEPHITHI", TypeName = "decimal(18, 2)")]
    public decimal Lephithi { get; set; }

    [Column("DONVICAP")]
    [StringLength(100)]
    public string? Donvicap { get; set; }

    [InverseProperty("IdChungchiNavigation")]
    public virtual ICollection<DkyChungchi> DkyChungchis { get; set; } = new List<DkyChungchi>();
}
