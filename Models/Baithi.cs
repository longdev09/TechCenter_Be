using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Baithi
{
    public int IdBaithi { get; set; }

    public string? Tieude { get; set; }

    public int? Loai { get; set; }

    public int? Thoiluong { get; set; }

    public DateTime? Ngaytao { get; set; }

    public int? Nguoitao { get; set; }

    public int? IdLop { get; set; }

    public string? Mota { get; set; }

    public DateOnly? Ngaybatdau { get; set; }

    public DateOnly? Ngayketthuc { get; set; }

    public int? IdLoaibaithi { get; set; }

    public virtual ICollection<Cauhoi> Cauhois { get; set; } = new List<Cauhoi>();

    public virtual LoaiBaithi? IdLoaibaithiNavigation { get; set; }

    public virtual Lophoc? IdLopNavigation { get; set; }
}
