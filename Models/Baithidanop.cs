using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("BAITHIDANOP")]
public partial class Baithidanop
{
    [Key]
    [Column("ID_BAITHI")]
    public int IdBaithi { get; set; }

    [Column("ID_DELUYEN")]
    public int IdDeluyen { get; set; }

    [Column("ID_HOCVIEN")]
    public int IdHocvien { get; set; }

    [Column("TGLAMBAI")]
    public int Tglambai { get; set; }

    [Column("NGAYLAMBAI", TypeName = "datetime")]
    public DateTime Ngaylambai { get; set; }

    [Column("DIEMSO")]
    public double Diemso { get; set; }

    [Column("SOCAUDUNG")]
    public int Socaudung { get; set; }

    [Column("SOCAUSAI")]
    public int Socausai { get; set; }

    [ForeignKey("IdDeluyen")]
    [InverseProperty("Baithidanops")]
    public virtual Deluyen IdDeluyenNavigation { get; set; } = null!;

    [ForeignKey("IdHocvien")]
    [InverseProperty("Baithidanops")]
    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    [ForeignKey("IdBaithi")]
    [InverseProperty("IdBaithis")]
    public virtual ICollection<Cauhoi> IdCauhois { get; set; } = new List<Cauhoi>();
}
