using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("THANHTOAN")]
public partial class Thanhtoan
{
    [Key]
    [Column("ID_THANHTOAN")]
    public int IdThanhtoan { get; set; }

    [Column("ID_DANGKY")]
    public int? IdDangky { get; set; }

    [Column("SOTIEN", TypeName = "decimal(18, 2)")]
    public decimal? Sotien { get; set; }

    [Column("PHUONGTHUCTT")]
    [StringLength(50)]
    public string? Phuongthuctt { get; set; }

    [Column("MAGIAODICH")]
    [StringLength(100)]
    public string? Magiaodich { get; set; }

    [Column("TRANGTHAI")]
    [StringLength(50)]
    public string? Trangthai { get; set; }

    [Column("NGAYTT", TypeName = "datetime")]
    public DateTime? Ngaytt { get; set; }

    [Column("NOIDUNG")]
    [StringLength(255)]
    public string? Noidung { get; set; }

    [ForeignKey("IdDangky")]
    [InverseProperty("Thanhtoans")]
    public virtual Dangkylop? IdDangkyNavigation { get; set; }
}
