using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class LichhocChitiet
{
    public int IdLhct { get; set; }

    public int IdLichhoc { get; set; }

    public int IdLoaikynang { get; set; }

    public DateOnly Ngayhoc { get; set; }

    public string? Tieudebuoihoc { get; set; }

    public string? Urlbuoihoc { get; set; }

    public virtual ICollection<Diemdanh> Diemdanhs { get; set; } = new List<Diemdanh>();

    public virtual Lichhoc IdLichhocNavigation { get; set; } = null!;

    public virtual Loaikynang IdLoaikynangNavigation { get; set; } = null!;
}
