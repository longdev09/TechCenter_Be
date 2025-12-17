using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class CauHoiCode
{
    public int IdCauHoi { get; set; }

    public string? CodeMau { get; set; }

    public string? NgonNgu { get; set; }

    public virtual Cauhoi IdCauHoiNavigation { get; set; } = null!;
}
