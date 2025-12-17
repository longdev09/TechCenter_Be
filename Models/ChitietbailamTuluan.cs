using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class ChitietbailamTuluan
{
    public int IdBailam { get; set; }

    public int IdKetqua { get; set; }

    public int IdCauhoi { get; set; }

    public string Noidungtl { get; set; } = null!;

    public double? DiemGv { get; set; }

    public string? NhanxetGv { get; set; }

    public DateTime? Ngaycham { get; set; }

    public virtual Cauhoi IdCauhoiNavigation { get; set; } = null!;

    public virtual Ketquathi IdKetquaNavigation { get; set; } = null!;
}
