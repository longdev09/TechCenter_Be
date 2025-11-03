using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("DELUYEN")]
public partial class Deluyen
{
    [Key]
    [Column("ID_DELUYEN")]
    public int IdDeluyen { get; set; }

    [Column("TENDELUYEN")]
    [StringLength(255)]
    public string Tendeluyen { get; set; } = null!;

    [Column("ID_LOPHOC")]
    public int IdLophoc { get; set; }

    [Column("ID_LOAIDE")]
    public int IdLoaide { get; set; }

    [Column("ID_TK")]
    public int IdTk { get; set; }

    [Column("THOIGIANLAM")]
    public int Thoigianlam { get; set; }

    [Column("NGAYTAODE", TypeName = "datetime")]
    public DateTime Ngaytaode { get; set; }

    [Column("IS_THICUOIKHOA")]
    public bool IsThicuoikhoa { get; set; }

    [InverseProperty("IdDeluyenNavigation")]
    public virtual ICollection<Baithidanop> Baithidanops { get; set; } = new List<Baithidanop>();

    [ForeignKey("IdLoaide")]
    [InverseProperty("Deluyens")]
    public virtual Loaide IdLoaideNavigation { get; set; } = null!;

    [ForeignKey("IdLophoc")]
    [InverseProperty("Deluyens")]
    public virtual Lophoc IdLophocNavigation { get; set; } = null!;

    [ForeignKey("IdTk")]
    [InverseProperty("Deluyens")]
    public virtual Taikhoan IdTkNavigation { get; set; } = null!;

    [ForeignKey("IdDeluyen")]
    [InverseProperty("IdDeluyens")]
    public virtual ICollection<Cauhoi> IdCauhois { get; set; } = new List<Cauhoi>();
}
