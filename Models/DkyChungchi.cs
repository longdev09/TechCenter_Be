using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class DkyChungchi
{
    public int IdDkycc { get; set; }

    public int IdHocvien { get; set; }

    public int IdChungchi { get; set; }

    public DateTime Ngaythicc { get; set; }

    public DateTime Ngaydangkythi { get; set; }

    public decimal Hotro { get; set; }

    public decimal Phithucte { get; set; }

    public string TtDangky { get; set; } = null!;

    public virtual Chungchi IdChungchiNavigation { get; set; } = null!;

    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;
}
