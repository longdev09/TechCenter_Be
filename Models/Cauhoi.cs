using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Cauhoi
{
    public int IdCauhoi { get; set; }

    public string Cauhoi1 { get; set; } = null!;

    public string? Dapandung { get; set; }

    public int? IdBaithi { get; set; }

    public virtual ICollection<Dapan> Dapans { get; set; } = new List<Dapan>();

    public virtual Baithi? IdBaithiNavigation { get; set; }
}
