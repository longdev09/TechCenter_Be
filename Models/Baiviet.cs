using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("BAIVIET")]
public partial class Baiviet
{
    [Key]
    [Column("ID_BAIVIET")]
    public int IdBaiviet { get; set; }

    [Column("TIEUDEBAIVIET")]
    [StringLength(255)]
    public string Tieudebaiviet { get; set; } = null!;

    [Column("NDBAIVIET")]
    public string? Ndbaiviet { get; set; }

    [Column("LUOTXEM")]
    public int Luotxem { get; set; }

    [Column("ID_KHOAHOC")]
    public int IdKhoahoc { get; set; }

    [ForeignKey("IdKhoahoc")]
    [InverseProperty("Baiviets")]
    public virtual Khoahoc IdKhoahocNavigation { get; set; } = null!;
}
