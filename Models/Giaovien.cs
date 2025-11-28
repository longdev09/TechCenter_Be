using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Giaovien
{
    public int IdGiaovien { get; set; }

    public int IdTaikhoan { get; set; }

    public string Hotengv { get; set; } = null!;

    public string? Gioitinhgv { get; set; }

    public DateOnly? Ngaysinhgv { get; set; }

    public string? Diachigv { get; set; }

    public virtual Taikhoan IdTaikhoanNavigation { get; set; } = null!;

    public virtual ICollection<LichhocChitiet> LichhocChitiets { get; set; } = new List<LichhocChitiet>();

    public virtual ICollection<Lophoc> Lophocs { get; set; } = new List<Lophoc>();

    public virtual ICollection<Nhanxettiendo> Nhanxettiendos { get; set; } = new List<Nhanxettiendo>();

    public virtual ICollection<Phancong> Phancongs { get; set; } = new List<Phancong>();
}
