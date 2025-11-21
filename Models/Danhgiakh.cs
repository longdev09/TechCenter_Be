using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("DANHGIAKH")]
[Index("IdKhoahoc", "IdHocvien", Name = "UQ_DANHGIAKH_KHOAHOC_HOCVIEN", IsUnique = true)]
public partial class Danhgiakh
{
    [Key]
    [Column("ID_DANHGIA")]
    public int IdDanhgia { get; set; }

    [Column("ID_KHOAHOC")]
    public int IdKhoahoc { get; set; }

    [Column("ID_HOCVIEN")]
    public int IdHocvien { get; set; }

    [Column("NOIDUNG")]
    [StringLength(150)]
    public string? Noidung { get; set; }

    [Column("SOSAO")]
    public int Sosao { get; set; }

    [Column("NGAYDANHGIA", TypeName = "datetime")]
    public DateTime? Ngaydanhgia { get; set; }

    [ForeignKey("IdHocvien")]
    [InverseProperty("Danhgiakhs")]
    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    [ForeignKey("IdKhoahoc")]
    [InverseProperty("Danhgiakhs")]
    public virtual Khoahoc IdKhoahocNavigation { get; set; } = null!;
}
