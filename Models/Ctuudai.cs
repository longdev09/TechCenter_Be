using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CTUUDAI")]
public partial class Ctuudai
{
    [Key]
    [Column("ID_UUDAI")]
    public int IdUudai { get; set; }

    [Column("TENCTUD")]
    [StringLength(255)]
    public string? Tenctud { get; set; }

    [Column("PHANTRAMUUDAI")]
    public int Phantramuudai { get; set; }

    [Column("NGAYBATDAU_UD")]
    public DateOnly NgaybatdauUd { get; set; }

    [Column("NGAYKETTHUC_UD")]
    public DateOnly NgayketthucUd { get; set; }

    [Column("SOLUOTTOIDA")]
    public int Soluottoida { get; set; }

    [Column("SOLUOTHIENTAI")]
    public int Soluothientai { get; set; }

    [Column("TRANGTHAI_UD")]
    [StringLength(20)]
    public string? TrangthaiUd { get; set; }

    [InverseProperty("IdUudaiNavigation")]
    public virtual ICollection<Dangkylop> Dangkylops { get; set; } = new List<Dangkylop>();
}
