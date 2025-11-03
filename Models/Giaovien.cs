using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("GIAOVIEN")]
[Index("IdTaikhoan", Name = "UQ__GIAOVIEN__EB942D7EE3A72230", IsUnique = true)]
public partial class Giaovien
{
    [Key]
    [Column("ID_GIAOVIEN")]
    public int IdGiaovien { get; set; }

    [Column("ID_TAIKHOAN")]
    public int IdTaikhoan { get; set; }

    [Column("HOTENGV")]
    [StringLength(100)]
    public string Hotengv { get; set; } = null!;

    [Column("GIOITINHGV")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Gioitinhgv { get; set; }

    [Column("NGAYSINHGV")]
    public DateOnly? Ngaysinhgv { get; set; }

    [Column("DIACHIGV")]
    [StringLength(255)]
    public string? Diachigv { get; set; }

    [Column("IS_ACTIVE")]
    public bool IsActive { get; set; }

    [ForeignKey("IdTaikhoan")]
    [InverseProperty("Giaovien")]
    public virtual Taikhoan IdTaikhoanNavigation { get; set; } = null!;

    [InverseProperty("IdGiaovienNavigation")]
    public virtual ICollection<Nhanxettiendo> Nhanxettiendos { get; set; } = new List<Nhanxettiendo>();

    [InverseProperty("IdGiaovienNavigation")]
    public virtual ICollection<Phancong> Phancongs { get; set; } = new List<Phancong>();
}
