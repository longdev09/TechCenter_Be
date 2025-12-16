using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CHITIETBAILAM_TULUAN")]
[Index("IdKetqua", "IdCauhoi", Name = "UQ_TULUAN", IsUnique = true)]
public partial class ChitietbailamTuluan
{
    [Key]
    [Column("ID_BAILAM")]
    public int IdBailam { get; set; }

    [Column("ID_KETQUA")]
    public int IdKetqua { get; set; }

    [Column("ID_CAUHOI")]
    public int IdCauhoi { get; set; }

    [Column("NOIDUNGTL")]
    public string Noidungtl { get; set; } = null!;

    [Column("DIEM_GV")]
    public double? DiemGv { get; set; }

    [Column("NHANXET_GV")]
    [StringLength(500)]
    public string? NhanxetGv { get; set; }

    [Column("NGAYCHAM", TypeName = "datetime")]
    public DateTime? Ngaycham { get; set; }

    [ForeignKey("IdCauhoi")]
    [InverseProperty("ChitietbailamTuluans")]
    public virtual Cauhoi IdCauhoiNavigation { get; set; } = null!;

    [ForeignKey("IdKetqua")]
    [InverseProperty("ChitietbailamTuluans")]
    public virtual Ketquathi IdKetquaNavigation { get; set; } = null!;
}
