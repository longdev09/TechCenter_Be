using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Lophoc
{
    public int IdLophoc { get; set; }

    public int IdKhoahoc { get; set; }

    public int Sisotoida { get; set; }

    public int Sisohientai { get; set; }

    public DateOnly Ngaykhaigiang { get; set; }

    public DateOnly Ngaybatdau { get; set; }

    public DateOnly Ngayketthuc { get; set; }

    public int? IdGvChinh { get; set; }

    public virtual ICollection<Baithi> Baithis { get; set; } = new List<Baithi>();

    public virtual ICollection<Dangkylop> Dangkylops { get; set; } = new List<Dangkylop>();

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();

    public virtual Giaovien? IdGvChinhNavigation { get; set; }

    public virtual Khoahoc IdKhoahocNavigation { get; set; } = null!;

    public virtual ICollection<Lichhoc> Lichhocs { get; set; } = new List<Lichhoc>();

    public virtual ICollection<Nhanxettiendo> Nhanxettiendos { get; set; } = new List<Nhanxettiendo>();

    public virtual ICollection<Phancong> Phancongs { get; set; } = new List<Phancong>();

    public virtual ICollection<Tailieu> IdTailieus { get; set; } = new List<Tailieu>();
}
