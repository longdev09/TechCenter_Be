using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CAUHOI")]
public partial class Cauhoi
{
    [Key]
    [Column("ID_CAUHOI")]
    public int IdCauhoi { get; set; }

    [Column("ID_BAITHI")]
    public int? IdBaithi { get; set; }

    [Column("ID_LOAICAUHOI")]
    public int? IdLoaicauhoi { get; set; }

    [Column("STT")]
    public int? Stt { get; set; }

    [Column("DIEM", TypeName = "decimal(5, 2)")]
    public decimal? Diem { get; set; }

    [Column("MUCDO")]
    [StringLength(255)]
    public string? Mucdo { get; set; }

    [Column("CAUHOI")]
    public string? Cauhoi1 { get; set; }

    [InverseProperty("IdCauHoiNavigation")]
    public virtual CauHoiCode? CauHoiCode { get; set; }

    [InverseProperty("IdCauhoiNavigation")]
    public virtual ICollection<Dapantracnghiem> Dapantracnghiems { get; set; } = new List<Dapantracnghiem>();

    [ForeignKey("IdBaithi")]
    [InverseProperty("Cauhois")]
    public virtual Baithi? IdBaithiNavigation { get; set; }

    [ForeignKey("IdLoaicauhoi")]
    [InverseProperty("Cauhois")]
    public virtual Loaicauhoi? IdLoaicauhoiNavigation { get; set; }
}
