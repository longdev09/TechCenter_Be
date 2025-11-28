using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class LichhocChitiet
{
    public int IdLhct { get; set; }

    public int IdLichhoc { get; set; }

    public DateOnly Ngayhoc { get; set; }

    public string? Tieudebuoihoc { get; set; }

    public string? Urlbuoihoc { get; set; }

    public int? IdGiaovien { get; set; }

    public virtual ICollection<Diemdanh> Diemdanhs { get; set; } = new List<Diemdanh>();

    public virtual Giaovien? IdGiaovienNavigation { get; set; }

    public virtual Lichhoc IdLichhocNavigation { get; set; } = null!;
}
