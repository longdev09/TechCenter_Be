using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Chitietbailamtracnghiem
{
    public int IdChitiet { get; set; }

    public int IdKetqua { get; set; }

    public int IdCauhoi { get; set; }

    public string? DapanChon { get; set; }

    public bool? IsDung { get; set; }

    public double Diem { get; set; }

    public virtual Cauhoi IdCauhoiNavigation { get; set; } = null!;

    public virtual Ketquathi IdKetquaNavigation { get; set; } = null!;
}
