using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("LOAICAUHOI")]
public partial class Loaicauhoi
{
    [Key]
    [Column("ID_LOAICAUHOI")]
    public int IdLoaicauhoi { get; set; }

    [Column("TEN_LOAI")]
    [StringLength(100)]
    public string TenLoai { get; set; } = null!;

    [Column("MOTA")]
    [StringLength(255)]
    public string? Mota { get; set; }

    [InverseProperty("IdLoaicauhoiNavigation")]
    public virtual ICollection<Cauhoi> Cauhois { get; set; } = new List<Cauhoi>();
}
