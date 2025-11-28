using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CAPDOKHOAHOC")]
public partial class Capdokhoahoc
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    public string TenCapDo { get; set; } = null!;

    [StringLength(255)]
    public string? MoTa { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? NgayTao { get; set; }

    public bool? TrangThai { get; set; }

    [StringLength(20)]
    public string? MauSac { get; set; }

    [InverseProperty("IdcapdokhoahocNavigation")]
    public virtual ICollection<Khoahoc> Khoahocs { get; set; } = new List<Khoahoc>();
}
