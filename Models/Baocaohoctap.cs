using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("BAOCAOHOCTAP")]
public partial class Baocaohoctap
{
    [Key]
    [Column("ID_BAOCAO")]
    public int IdBaocao { get; set; }

    [Column("ID_HOCVIEN")]
    public int IdHocvien { get; set; }

    [Column("ID_KHOAHOC")]
    public int IdKhoahoc { get; set; }

    [Column("SOBUOIVANG")]
    public int Sobuoivang { get; set; }

    [Column("DELUYENDALAM")]
    public int Deluyendalam { get; set; }

    [Column("DIEMTB")]
    public int? Diemtb { get; set; }

    [ForeignKey("IdHocvien")]
    [InverseProperty("Baocaohoctaps")]
    public virtual Hocvien IdHocvienNavigation { get; set; } = null!;

    [ForeignKey("IdKhoahoc")]
    [InverseProperty("Baocaohoctaps")]
    public virtual Khoahoc IdKhoahocNavigation { get; set; } = null!;
}
