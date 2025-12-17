using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Lichhoc
{
    public int IdLichhoc { get; set; }

    public int IdLophoc { get; set; }

    public int? Thu { get; set; }

    public DateTime Giobatdau { get; set; }

    public DateTime Gioketthuc { get; set; }

    public virtual Lophoc IdLophocNavigation { get; set; } = null!;

    public virtual ICollection<LichhocChitiet> LichhocChitiets { get; set; } = new List<LichhocChitiet>();
}
