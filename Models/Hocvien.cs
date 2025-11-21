using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("HOCVIEN")]
[Index("IdTaikhoan", Name = "UQ__HOCVIEN__EB942D7EF1E73928", IsUnique = true)]
public partial class Hocvien
{
    [Key]
    [Column("ID_HOCVIEN")]
    public int IdHocvien { get; set; }

    [Column("ID_TAIKHOAN")]
    public int IdTaikhoan { get; set; }

    [Column("HOTENHV")]
    [StringLength(100)]
    public string Hotenhv { get; set; } = null!;

    [Column("GIOITINHHV")]
    [StringLength(3)]
    public string? Gioitinhhv { get; set; }

    [Column("NGAYSINHHV")]
    public DateOnly? Ngaysinhhv { get; set; }

    [Column("DIACHIHV")]
    [StringLength(255)]
    public string? Diachihv { get; set; }

    [InverseProperty("IdHocvienNavigation")]
    public virtual ICollection<Baithidanop> Baithidanops { get; set; } = new List<Baithidanop>();

    [InverseProperty("IdHocvienNavigation")]
    public virtual ICollection<Baocaohoctap> Baocaohoctaps { get; set; } = new List<Baocaohoctap>();

    [InverseProperty("IdHvNavigation")]
    public virtual ICollection<Dangkylop> Dangkylops { get; set; } = new List<Dangkylop>();

    [InverseProperty("IdHocvienNavigation")]
    public virtual ICollection<Danhgiakh> Danhgiakhs { get; set; } = new List<Danhgiakh>();

    [InverseProperty("IdHvNavigation")]
    public virtual ICollection<Diemdanh> Diemdanhs { get; set; } = new List<Diemdanh>();

    [InverseProperty("IdHocvienNavigation")]
    public virtual ICollection<DkyChungchi> DkyChungchis { get; set; } = new List<DkyChungchi>();

    [ForeignKey("IdTaikhoan")]
    [InverseProperty("Hocvien")]
    public virtual Taikhoan IdTaikhoanNavigation { get; set; } = null!;

    [InverseProperty("IdHocvienNavigation")]
    public virtual ICollection<Nhanxettiendo> Nhanxettiendos { get; set; } = new List<Nhanxettiendo>();
}
