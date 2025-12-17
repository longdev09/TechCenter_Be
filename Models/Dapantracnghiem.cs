using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Dapantracnghiem
{
    public int IdDapan { get; set; }

    public int IdCauhoi { get; set; }

    public string Ma { get; set; } = null!;

    public bool? Isdung { get; set; }

    public string? Cauhoidapan { get; set; }

    public virtual Cauhoi IdCauhoiNavigation { get; set; } = null!;
}
