using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Hoadon
{
    public int Idhoadon { get; set; }

    public string Mahoadon { get; set; } = null!;

    public int IdHocvien { get; set; }

    public int IdLophoc { get; set; }

    public DateTime? Ngaytao { get; set; }

    public decimal Tongtien { get; set; }

    public string? Trangthai { get; set; }

    public string? Ghichu { get; set; }

    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    public virtual Lophoc IdLophocNavigation { get; set; } = null!;
}
