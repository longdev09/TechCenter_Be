using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Baocaohoctap
{
    public int IdBaocao { get; set; }

    public int IdHocvien { get; set; }

    public int IdKhoahoc { get; set; }

    public int Sobuoivang { get; set; }

    public int Deluyendalam { get; set; }

    public int? Diemtb { get; set; }

    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    public virtual Khoahoc IdKhoahocNavigation { get; set; } = null!;
}
