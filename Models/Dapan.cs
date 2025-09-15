using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Dapan
{
    public int IdDapan { get; set; }

    public int IdCauhoi { get; set; }

    public string NdDapan { get; set; } = null!;

    public bool IsTrue { get; set; }

    public virtual Cauhoi IdCauhoiNavigation { get; set; } = null!;
}
