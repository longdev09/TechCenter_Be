using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CAUHOI")]
public partial class Cauhoi
{
    [Key]
    [Column("ID_CAUHOI")]
    public int IdCauhoi { get; set; }

    [Column("CAUHOI")]
    [StringLength(255)]
    public string Cauhoi1 { get; set; } = null!;

    [Column("DAPANDUNG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? Dapandung { get; set; }

    [Column("ID_BAITHI")]
    public int? IdBaithi { get; set; }

    [Column("ID_LOAICAUHOI")]
    public int? IdLoaicauhoi { get; set; }

    [ForeignKey("IdBaithi")]
    [InverseProperty("Cauhois")]
    public virtual Baithi? IdBaithiNavigation { get; set; }

    [ForeignKey("IdLoaicauhoi")]
    [InverseProperty("Cauhois")]
    public virtual Loaicauhoi? IdLoaicauhoiNavigation { get; set; }
}
