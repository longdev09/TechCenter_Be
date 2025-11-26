using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("HOADON")]
public partial class Hoadon
{
    [Key]
    [Column("IDHOADON")]
    public int Idhoadon { get; set; }

    [Column("MAHOADON")]
    [StringLength(50)]
    [Unicode(false)]
    public string Mahoadon { get; set; } = null!;

    [Column("ID_HOCVIEN")]
    public int IdHocvien { get; set; }

    [Column("ID_LOPHOC")]
    public int IdLophoc { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [Column("TONGTIEN", TypeName = "decimal(18, 2)")]
    public decimal Tongtien { get; set; }

    [Column("TRANGTHAI")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Trangthai { get; set; }

    [Column("GHICHU")]
    [StringLength(500)]
    public string? Ghichu { get; set; }

    [ForeignKey("IdHocvien")]
    [InverseProperty("Hoadons")]
    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    [ForeignKey("IdLophoc")]
    [InverseProperty("Hoadons")]
    public virtual Lophoc IdLophocNavigation { get; set; } = null!;
}
