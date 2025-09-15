using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Khoahoc
{
    public int IdKhoahoc { get; set; }

    public string Tenkhoahoc { get; set; } = null!;

    public string? Motakh { get; set; }

    public decimal? Hocphi { get; set; }

    public string? Anhdaidien { get; set; }

    public virtual ICollection<Baiviet> Baiviets { get; set; } = new List<Baiviet>();

    public virtual ICollection<Baocaohoctap> Baocaohoctaps { get; set; } = new List<Baocaohoctap>();

    public virtual ICollection<Lophoc> Lophocs { get; set; } = new List<Lophoc>();
}
