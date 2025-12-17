using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Ctuudai
{
    public int IdUudai { get; set; }

    public string? Tenctud { get; set; }

    public int Phantramuudai { get; set; }

    public DateOnly NgaybatdauUd { get; set; }

    public DateOnly NgayketthucUd { get; set; }

    public int Soluottoida { get; set; }

    public int Soluothientai { get; set; }

    public string? TrangthaiUd { get; set; }

    public virtual ICollection<Dangkylop> Dangkylops { get; set; } = new List<Dangkylop>();
}
