using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Capdokhoahoc
{
    public int Id { get; set; }

    public string TenCapDo { get; set; } = null!;

    public string? MoTa { get; set; }

    public DateTime? NgayTao { get; set; }

    public bool? TrangThai { get; set; }

    public string? MauSac { get; set; }

    public virtual ICollection<Khoahoc> Khoahocs { get; set; } = new List<Khoahoc>();
}
