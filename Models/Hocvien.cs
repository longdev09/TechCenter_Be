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

    public string? AnhHv { get; set; }

    public virtual ICollection<Baocaohoctap> Baocaohoctaps { get; set; } = new List<Baocaohoctap>();

    public virtual ICollection<Dangkylop> Dangkylops { get; set; } = new List<Dangkylop>();

    public virtual ICollection<Danhgiakh> Danhgiakhs { get; set; } = new List<Danhgiakh>();

    public virtual ICollection<Diemdanh> Diemdanhs { get; set; } = new List<Diemdanh>();

    public virtual ICollection<DkyChungchi> DkyChungchis { get; set; } = new List<DkyChungchi>();

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    public virtual Taikhoan IdTaikhoanNavigation { get; set; } = null!;

    public virtual ICollection<Ketquathi> Ketquathis { get; set; } = new List<Ketquathi>();

    public virtual ICollection<Nhanxettiendo> Nhanxettiendos { get; set; } = new List<Nhanxettiendo>();
}
