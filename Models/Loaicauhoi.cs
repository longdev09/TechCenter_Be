using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Loaicauhoi
{
    public int IdLoaicauhoi { get; set; }

    public string TenLoai { get; set; } = null!;

    public string? Mota { get; set; }

    public virtual ICollection<Cauhoi> Cauhois { get; set; } = new List<Cauhoi>();
}
