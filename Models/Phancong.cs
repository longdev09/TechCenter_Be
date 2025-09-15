using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Phancong
{
    public int IdGiaovien { get; set; }

    public int IdLophoc { get; set; }

    public DateOnly Ngayphancong { get; set; }

    public virtual Giaovien IdGiaovienNavigation { get; set; } = null!;

    public virtual Lophoc IdLophocNavigation { get; set; } = null!;
}
