using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Baiviet
{
    public int IdBaiviet { get; set; }

    public string Tieudebaiviet { get; set; } = null!;

    public string? Ndbaiviet { get; set; }

    public int Luotxem { get; set; }

    public int IdKhoahoc { get; set; }

    public virtual Khoahoc IdKhoahocNavigation { get; set; } = null!;
}
