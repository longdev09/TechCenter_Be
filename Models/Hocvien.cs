using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Hocvien
{
    public int IdHocvien { get; set; }

    public int IdTaikhoan { get; set; }

    public string Hotenhv { get; set; } = null!;

    public string? Gioitinhhv { get; set; }

    public DateOnly? Ngaysinhhv { get; set; }

    public string? Diachihv { get; set; }

    public virtual ICollection<Baithidanop> Baithidanops { get; set; } = new List<Baithidanop>();

    public virtual ICollection<Baocaohoctap> Baocaohoctaps { get; set; } = new List<Baocaohoctap>();

    public virtual ICollection<Dangkylop> Dangkylops { get; set; } = new List<Dangkylop>();

    public virtual ICollection<Diemdanh> Diemdanhs { get; set; } = new List<Diemdanh>();

    public virtual ICollection<DkyChungchi> DkyChungchis { get; set; } = new List<DkyChungchi>();

    public virtual Taikhoan IdTaikhoanNavigation { get; set; } = null!;

    public virtual ICollection<Nhanxettiendo> Nhanxettiendos { get; set; } = new List<Nhanxettiendo>();
}
