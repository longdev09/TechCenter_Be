using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Khoahoc
{
    public int IdKhoahoc { get; set; }

    public string Tenkhoahoc { get; set; } = null!;

    public decimal? Hocphi { get; set; }

    public string? Anhdaidien { get; set; }

    public int? Idcapdokhoahoc { get; set; }

    public int? IdLoaikynang { get; set; }

    public string? Motakh { get; set; }

    public string? Ketquadatduoc { get; set; }

    public string? Noidungkhoahoc { get; set; }

    public decimal Diemdanhgia { get; set; }

    public virtual ICollection<Baiviet> Baiviets { get; set; } = new List<Baiviet>();

    public virtual ICollection<Baocaohoctap> Baocaohoctaps { get; set; } = new List<Baocaohoctap>();

    public virtual ICollection<Danhgiakh> Danhgiakhs { get; set; } = new List<Danhgiakh>();

    public virtual Loaikynang? IdLoaikynangNavigation { get; set; }

    public virtual Capdokhoahoc? IdcapdokhoahocNavigation { get; set; }

    public virtual ICollection<Lophoc> Lophocs { get; set; } = new List<Lophoc>();
}
