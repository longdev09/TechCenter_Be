using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("DANGKYLOP")]
public partial class Dangkylop
{
    [Key]
    [Column("ID_DANGKY")]
    public int IdDangky { get; set; }

    [Column("ID_LOPHOC")]
    public int IdLophoc { get; set; }

    [Column("ID_HV")]
    public int IdHv { get; set; }

    [Column("ID_UUDAI")]
    public int? IdUudai { get; set; }

    [Column("NGAYDANGKY", TypeName = "datetime")]
    public DateTime Ngaydangky { get; set; }

    [Column("SOTIENGIAM", TypeName = "decimal(18, 2)")]
    public decimal Sotiengiam { get; set; }

    [Column("TONGTIEN", TypeName = "decimal(18, 2)")]
    public decimal Tongtien { get; set; }

    [ForeignKey("IdHv")]
    [InverseProperty("Dangkylops")]
    public virtual Hocvien IdHvNavigation { get; set; } = null!;

    [ForeignKey("IdLophoc")]
    [InverseProperty("Dangkylops")]
    public virtual Lophoc IdLophocNavigation { get; set; } = null!;

    [ForeignKey("IdUudai")]
    [InverseProperty("Dangkylops")]
    public virtual Ctuudai? IdUudaiNavigation { get; set; }
}
