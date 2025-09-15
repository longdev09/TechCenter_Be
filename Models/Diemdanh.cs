using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Diemdanh
{
    public int IdLhct { get; set; }

    public int IdHv { get; set; }

    public bool TtDiemdanh { get; set; }

    public virtual Hocvien IdHvNavigation { get; set; } = null!;

    public virtual LichhocChitiet IdLhctNavigation { get; set; } = null!;
}
