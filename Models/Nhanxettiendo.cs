using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("NHANXETTIENDO")]
public partial class Nhanxettiendo
{
    [Key]
    [Column("ID_NHANXET")]
    public int IdNhanxet { get; set; }

    [Column("ID_HOCVIEN")]
    public int IdHocvien { get; set; }

    [Column("ID_GIAOVIEN")]
    public int IdGiaovien { get; set; }

    [Column("ID_LOPHOC")]
    public int IdLophoc { get; set; }

    [Column("NHANXET")]
    [StringLength(255)]
    public string? Nhanxet { get; set; }

    [Column("NGAYNHANXET", TypeName = "datetime")]
    public DateTime? Ngaynhanxet { get; set; }

    [ForeignKey("IdGiaovien")]
    [InverseProperty("Nhanxettiendos")]
    public virtual Giaovien IdGiaovienNavigation { get; set; } = null!;

    [ForeignKey("IdHocvien")]
    [InverseProperty("Nhanxettiendos")]
    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    [ForeignKey("IdLophoc")]
    [InverseProperty("Nhanxettiendos")]
    public virtual Lophoc IdLophocNavigation { get; set; } = null!;
}
