using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("LOAI_BAITHI")]
public partial class LoaiBaithi
{
    [Key]
    [Column("ID_LOAIBAITHI")]
    public int IdLoaibaithi { get; set; }

    [Column("TENLOAI")]
    [StringLength(100)]
    public string? Tenloai { get; set; }

    [InverseProperty("IdLoaibaithiNavigation")]
    public virtual ICollection<Baithi> Baithis { get; set; } = new List<Baithi>();
}
