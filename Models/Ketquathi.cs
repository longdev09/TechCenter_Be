using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("KETQUATHI")]
public partial class Ketquathi
{
    [Key]
    [Column("ID_KETQUA")]
    public int IdKetqua { get; set; }

    [Column("ID_HOCVIEN")]
    public int IdHocvien { get; set; }

    [Column("ID_BAITHI")]
    public int IdBaithi { get; set; }

    [Column("LAN_THI")]
    public int LanThi { get; set; }

    [Column("THOIGIANBATDAU", TypeName = "datetime")]
    public DateTime Thoigianbatdau { get; set; }

    [Column("THOIGIANKETTHUC", TypeName = "datetime")]
    public DateTime? Thoigianketthuc { get; set; }

    [Column("TONGDIEM")]
    public double? Tongdiem { get; set; }

    [Column("TRANGTHAI")]
    [StringLength(20)]
    public string Trangthai { get; set; } = null!;

    [Column("TRANGTHAI_CHAM")]
    [StringLength(20)]
    public string TrangthaiCham { get; set; } = null!;

    [Column("NGAYTHI", TypeName = "datetime")]
    public DateTime Ngaythi { get; set; }

    [InverseProperty("IdKetquaNavigation")]
    public virtual ICollection<ChitietbailamTuluan> ChitietbailamTuluans { get; set; } = new List<ChitietbailamTuluan>();

    [InverseProperty("IdKetquaNavigation")]
    public virtual ICollection<Chitietbailamtracnghiem> Chitietbailamtracnghiems { get; set; } = new List<Chitietbailamtracnghiem>();

    [ForeignKey("IdBaithi")]
    [InverseProperty("Ketquathis")]
    public virtual Baithi IdBaithiNavigation { get; set; } = null!;

    [ForeignKey("IdHocvien")]
    [InverseProperty("Ketquathis")]
    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;
}
