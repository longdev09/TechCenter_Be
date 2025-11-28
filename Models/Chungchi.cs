using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Chungchi
{
    public int IdChungchi { get; set; }

    public string Tenchungchi { get; set; } = null!;

    public string? Motacc { get; set; }

    public decimal Lephithi { get; set; }

    public string? Donvicap { get; set; }

    public virtual ICollection<DkyChungchi> DkyChungchis { get; set; } = new List<DkyChungchi>();
}
