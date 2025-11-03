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

    [InverseProperty("IdCauhoiNavigation")]
    public virtual ICollection<Dapan> Dapans { get; set; } = new List<Dapan>();

    [ForeignKey("IdCauhoi")]
    [InverseProperty("IdCauhois")]
    public virtual ICollection<Baithidanop> IdBaithis { get; set; } = new List<Baithidanop>();

    [ForeignKey("IdCauhoi")]
    [InverseProperty("IdCauhois")]
    public virtual ICollection<Deluyen> IdDeluyens { get; set; } = new List<Deluyen>();
}
