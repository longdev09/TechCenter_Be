using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Dangkylop
{
    public int IdDangky { get; set; }

    public int IdLophoc { get; set; }

    public int IdHv { get; set; }

    public int? IdUudai { get; set; }

    public DateTime Ngaydangky { get; set; }

    public decimal Sotiengiam { get; set; }

    public decimal Tongtien { get; set; }

    public virtual Hocvien IdHvNavigation { get; set; } = null!;

    public virtual Lophoc IdLophocNavigation { get; set; } = null!;

    public virtual Ctuudai? IdUudaiNavigation { get; set; }
}
