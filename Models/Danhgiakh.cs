using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Danhgiakh
{
    public int IdDanhgia { get; set; }

    public int IdKhoahoc { get; set; }

    public int IdHocvien { get; set; }

    public string? Noidung { get; set; }

    public int Sosao { get; set; }

    public DateTime? Ngaydanhgia { get; set; }

    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    public virtual Khoahoc IdKhoahocNavigation { get; set; } = null!;
}
