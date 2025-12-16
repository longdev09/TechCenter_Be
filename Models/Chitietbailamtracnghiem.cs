using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CHITIETBAILAMTRACNGHIEM")]
[Index("IdKetqua", "IdCauhoi", Name = "UQ_KETQUA_CAUHOI", IsUnique = true)]
public partial class Chitietbailamtracnghiem
{
    [Key]
    [Column("ID_CHITIET")]
    public int IdChitiet { get; set; }

    [Column("ID_KETQUA")]
    public int IdKetqua { get; set; }

    [Column("ID_CAUHOI")]
    public int IdCauhoi { get; set; }

    [Column("DAPAN_CHON")]
    [StringLength(10)]
    public string? DapanChon { get; set; }

    [Column("IS_DUNG")]
    public bool? IsDung { get; set; }

    [Column("DIEM")]
    public double Diem { get; set; }

    [ForeignKey("IdCauhoi")]
    [InverseProperty("Chitietbailamtracnghiems")]
    public virtual Cauhoi IdCauhoiNavigation { get; set; } = null!;

    [ForeignKey("IdKetqua")]
    [InverseProperty("Chitietbailamtracnghiems")]
    public virtual Ketquathi IdKetquaNavigation { get; set; } = null!;
}
