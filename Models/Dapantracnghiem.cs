using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("DAPANTRACNGHIEM")]
public partial class Dapantracnghiem
{
    [Key]
    [Column("ID_DAPAN")]
    public int IdDapan { get; set; }

    [Column("ID_CAUHOI")]
    public int IdCauhoi { get; set; }

    [StringLength(10)]
    public string Ma { get; set; } = null!;

    [Column("ISDUNG")]
    public bool? Isdung { get; set; }

    [Column("CAUHOIDAPAN")]
    [StringLength(500)]
    public string? Cauhoidapan { get; set; }

    [ForeignKey("IdCauhoi")]
    [InverseProperty("Dapantracnghiems")]
    public virtual Cauhoi IdCauhoiNavigation { get; set; } = null!;
}
