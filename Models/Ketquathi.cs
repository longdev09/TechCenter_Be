using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Ketquathi
{
    public int IdKetqua { get; set; }

    public int IdHocvien { get; set; }

    public int IdBaithi { get; set; }

    public int LanThi { get; set; }

    public DateTime Thoigianbatdau { get; set; }

    public DateTime? Thoigianketthuc { get; set; }

    public double? Tongdiem { get; set; }

    public string Trangthai { get; set; } = null!;

    public string TrangthaiCham { get; set; } = null!;

    public DateTime Ngaythi { get; set; }

    public virtual ICollection<ChitietbailamTuluan> ChitietbailamTuluans { get; set; } = new List<ChitietbailamTuluan>();

    public virtual ICollection<Chitietbailamtracnghiem> Chitietbailamtracnghiems { get; set; } = new List<Chitietbailamtracnghiem>();

    public virtual Baithi IdBaithiNavigation { get; set; } = null!;

    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;
}
