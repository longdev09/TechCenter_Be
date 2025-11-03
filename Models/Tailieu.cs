using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("TAILIEU")]
public partial class Tailieu
{
    [Key]
    [Column("ID_TAILIEU")]
    public int IdTailieu { get; set; }

    [Column("ID_GV")]
    public int IdGv { get; set; }

    [Column("TIEUDETL")]
    [StringLength(255)]
    public string Tieudetl { get; set; } = null!;

    [Column("MOTATL")]
    public string? Motatl { get; set; }

    [Column("URLTAILIEU")]
    public string? Urltailieu { get; set; }

    [Column("NGAYDANGTL", TypeName = "datetime")]
    public DateTime Ngaydangtl { get; set; }

    [Column("IS_PUBLIC")]
    public bool IsPublic { get; set; }

    [ForeignKey("IdGv")]
    [InverseProperty("Tailieus")]
    public virtual Taikhoan IdGvNavigation { get; set; } = null!;

    [ForeignKey("IdTailieu")]
    [InverseProperty("IdTailieus")]
    public virtual ICollection<Lophoc> IdLophocs { get; set; } = new List<Lophoc>();
}
