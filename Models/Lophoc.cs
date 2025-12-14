using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("LOPHOC")]
public partial class Lophoc
{
    [Key]
    [Column("ID_LOPHOC")]
    public int IdLophoc { get; set; }

    [Column("ID_KHOAHOC")]
    public int IdKhoahoc { get; set; }

    [Column("SISOTOIDA")]
    public int Sisotoida { get; set; }

    [Column("SISOHIENTAI")]
    public int Sisohientai { get; set; }

    [Column("NGAYKHAIGIANG")]
    public DateOnly Ngaykhaigiang { get; set; }

    [Column("NGAYBATDAU")]
    public DateOnly Ngaybatdau { get; set; }

    [Column("NGAYKETTHUC")]
    public DateOnly Ngayketthuc { get; set; }

    [InverseProperty("IdLopNavigation")]
    public virtual ICollection<Baithi> Baithis { get; set; } = new List<Baithi>();

    [InverseProperty("IdLophocNavigation")]
    public virtual ICollection<Dangkylop> Dangkylops { get; set; } = new List<Dangkylop>();

    [InverseProperty("IdLophocNavigation")]
    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    [ForeignKey("IdKhoahoc")]
    [InverseProperty("Lophocs")]
    public virtual Khoahoc IdKhoahocNavigation { get; set; } = null!;

    [InverseProperty("IdLophocNavigation")]
    public virtual ICollection<Lichhoc> Lichhocs { get; set; } = new List<Lichhoc>();

    [InverseProperty("IdLophocNavigation")]
    public virtual ICollection<Nhanxettiendo> Nhanxettiendos { get; set; } = new List<Nhanxettiendo>();

    [InverseProperty("IdLophocNavigation")]
    public virtual ICollection<Phancong> Phancongs { get; set; } = new List<Phancong>();

    [ForeignKey("IdLophoc")]
    [InverseProperty("IdLophocs")]
    public virtual ICollection<Tailieu> IdTailieus { get; set; } = new List<Tailieu>();
}
