using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Deluyen
{
    public int IdDeluyen { get; set; }

    public string Tendeluyen { get; set; } = null!;

    public int IdLophoc { get; set; }

    public int IdLoaide { get; set; }

    public int IdTk { get; set; }

    public int Thoigianlam { get; set; }

    public DateTime Ngaytaode { get; set; }

    public bool IsThicuoikhoa { get; set; }

    public virtual ICollection<Baithidanop> Baithidanops { get; set; } = new List<Baithidanop>();

    public virtual Loaide IdLoaideNavigation { get; set; } = null!;

    public virtual Lophoc IdLophocNavigation { get; set; } = null!;

    public virtual Taikhoan IdTkNavigation { get; set; } = null!;

    public virtual ICollection<Cauhoi> IdCauhois { get; set; } = new List<Cauhoi>();
}
