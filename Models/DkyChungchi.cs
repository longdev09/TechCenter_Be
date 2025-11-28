using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("DKY_CHUNGCHI")]
public partial class DkyChungchi
{
    [Key]
    [Column("ID_DKYCC")]
    public int IdDkycc { get; set; }

    [Column("ID_HOCVIEN")]
    public int IdHocvien { get; set; }

    [Column("ID_CHUNGCHI")]
    public int IdChungchi { get; set; }

    [Column("NGAYTHICC", TypeName = "datetime")]
    public DateTime Ngaythicc { get; set; }

    [Column("NGAYDANGKYTHI", TypeName = "datetime")]
    public DateTime Ngaydangkythi { get; set; }

    [Column("HOTRO", TypeName = "decimal(18, 2)")]
    public decimal Hotro { get; set; }

    [Column("PHITHUCTE", TypeName = "decimal(18, 2)")]
    public decimal Phithucte { get; set; }

    [Column("TT_DANGKY")]
    [StringLength(20)]
    public string TtDangky { get; set; } = null!;

    [ForeignKey("IdChungchi")]
    [InverseProperty("DkyChungchis")]
    public virtual Chungchi IdChungchiNavigation { get; set; } = null!;

    [ForeignKey("IdHocvien")]
    [InverseProperty("DkyChungchis")]
    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;
}
