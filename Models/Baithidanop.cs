using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Baithidanop
{
    public int IdBaithi { get; set; }

    public int IdDeluyen { get; set; }

    public int IdHocvien { get; set; }

    public int Tglambai { get; set; }

    public DateTime Ngaylambai { get; set; }

    public double Diemso { get; set; }

    public int Socaudung { get; set; }

    public int Socausai { get; set; }

    public virtual Deluyen IdDeluyenNavigation { get; set; } = null!;

    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    public virtual ICollection<Cauhoi> IdCauhois { get; set; } = new List<Cauhoi>();
}
