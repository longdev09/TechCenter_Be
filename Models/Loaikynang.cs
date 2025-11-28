using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Loaikynang
{
    public int IdLoaikynang { get; set; }

    public string Tenloaikynang { get; set; } = null!;

    public virtual ICollection<Khoahoc> Khoahocs { get; set; } = new List<Khoahoc>();
}
