using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("BAITHI")]
public partial class Baithi
{
    [Key]
    [Column("ID_BAITHI")]
    public int IdBaithi { get; set; }

    [Column("TIEUDE")]
    [StringLength(255)]
    public string? Tieude { get; set; }

    [Column("LOAI")]
    public int? Loai { get; set; }

    [Column("THOILUONG")]
    public int? Thoiluong { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [Column("NGUOITAO")]
    public int? Nguoitao { get; set; }

    [Column("ID_LOP")]
    public int? IdLop { get; set; }

    [InverseProperty("IdBaithiNavigation")]
    public virtual ICollection<Cauhoi> Cauhois { get; set; } = new List<Cauhoi>();

    [ForeignKey("IdLop")]
    [InverseProperty("Baithis")]
    public virtual Lophoc? IdLopNavigation { get; set; }
}
