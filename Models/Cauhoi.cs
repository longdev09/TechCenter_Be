using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Cauhoi
{
    public int IdCauhoi { get; set; }

    public int? IdBaithi { get; set; }

    public int? IdLoaicauhoi { get; set; }

    public int? Stt { get; set; }

    public decimal? Diem { get; set; }

    public string? Mucdo { get; set; }

    public string? Cauhoi1 { get; set; }

    public virtual CauHoiCode? CauHoiCode { get; set; }

    public virtual ICollection<ChitietbailamTuluan> ChitietbailamTuluans { get; set; } = new List<ChitietbailamTuluan>();

    public virtual ICollection<Chitietbailamtracnghiem> Chitietbailamtracnghiems { get; set; } = new List<Chitietbailamtracnghiem>();

    public virtual ICollection<Dapantracnghiem> Dapantracnghiems { get; set; } = new List<Dapantracnghiem>();

    public virtual Baithi? IdBaithiNavigation { get; set; }

    public virtual Loaicauhoi? IdLoaicauhoiNavigation { get; set; }
}
