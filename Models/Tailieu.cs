using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Tailieu
{
    public int IdTailieu { get; set; }

    public string Tieudetl { get; set; } = null!;

    public string? Motatl { get; set; }

    public string? Urltailieu { get; set; }

    public DateTime Ngaydangtl { get; set; }

    public bool IsPublic { get; set; }

    public virtual ICollection<Lophoc> IdLophocs { get; set; } = new List<Lophoc>();
}
