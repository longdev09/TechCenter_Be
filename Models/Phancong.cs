using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[PrimaryKey("IdGiaovien", "IdLophoc")]
[Table("PHANCONG")]
[Index("IdGiaovien", "IdLophoc", Name = "UQ_PHANCONG", IsUnique = true)]
public partial class Phancong
{
    [Key]
    [Column("ID_GIAOVIEN")]
    public int IdGiaovien { get; set; }

    [Key]
    [Column("ID_LOPHOC")]
    public int IdLophoc { get; set; }

    [Column("NGAYPHANCONG")]
    public DateOnly Ngayphancong { get; set; }

    [ForeignKey("IdGiaovien")]
    [InverseProperty("Phancongs")]
    public virtual Giaovien IdGiaovienNavigation { get; set; } = null!;

    [ForeignKey("IdLophoc")]
    [InverseProperty("Phancongs")]
    public virtual Lophoc IdLophocNavigation { get; set; } = null!;
}
