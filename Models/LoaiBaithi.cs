using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class LoaiBaithi
{
    public int IdLoaibaithi { get; set; }

    public string? Tenloai { get; set; }

    public virtual ICollection<Baithi> Baithis { get; set; } = new List<Baithi>();
}
