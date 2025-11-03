using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("KHOAHOC")]
[Index("Tenkhoahoc", Name = "UQ__KHOAHOC__9FC17498E96D9490", IsUnique = true)]
public partial class Khoahoc
{
    [Key]
    [Column("ID_KHOAHOC")]
    public int IdKhoahoc { get; set; }

    [Column("TENKHOAHOC")]
    [StringLength(100)]
    public string Tenkhoahoc { get; set; } = null!;

    [Column("MOTAKH")]
    public string? Motakh { get; set; }

    [Column("HOCPHI", TypeName = "decimal(18, 2)")]
    public decimal? Hocphi { get; set; }

    [Column("ANHDAIDIEN")]
    [StringLength(255)]
    public string? Anhdaidien { get; set; }

    [Column("IDCAPDOKHOAHOC")]
    public int? Idcapdokhoahoc { get; set; }

    [InverseProperty("IdKhoahocNavigation")]
    public virtual ICollection<Baiviet> Baiviets { get; set; } = new List<Baiviet>();

    [InverseProperty("IdKhoahocNavigation")]
    public virtual ICollection<Baocaohoctap> Baocaohoctaps { get; set; } = new List<Baocaohoctap>();

    [ForeignKey("Idcapdokhoahoc")]
    [InverseProperty("Khoahocs")]
    public virtual Capdokhoahoc? IdcapdokhoahocNavigation { get; set; }

    [InverseProperty("IdKhoahocNavigation")]
    public virtual ICollection<Lophoc> Lophocs { get; set; } = new List<Lophoc>();
}
