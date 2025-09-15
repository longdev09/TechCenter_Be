using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Nhanxettiendo
{
    public int IdNhanxet { get; set; }

    public int IdHocvien { get; set; }

    public int IdGiaovien { get; set; }

    public int IdLophoc { get; set; }

    public string? Nhanxet { get; set; }

    public DateTime? Ngaynhanxet { get; set; }

    public virtual Giaovien IdGiaovienNavigation { get; set; } = null!;

    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    public virtual Lophoc IdLophocNavigation { get; set; } = null!;
}
