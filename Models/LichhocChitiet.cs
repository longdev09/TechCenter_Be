using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("LICHHOC_CHITIET")]
public partial class LichhocChitiet
{
    [Key]
    [Column("ID_LHCT")]
    public int IdLhct { get; set; }

    [Column("ID_LICHHOC")]
    public int IdLichhoc { get; set; }

    [Column("ID_LOAIKYNANG")]
    public int IdLoaikynang { get; set; }

    [Column("NGAYHOC")]
    public DateOnly Ngayhoc { get; set; }

    [Column("TIEUDEBUOIHOC")]
    [StringLength(255)]
    public string? Tieudebuoihoc { get; set; }

    [Column("URLBUOIHOC")]
    [StringLength(255)]
    public string? Urlbuoihoc { get; set; }

    [InverseProperty("IdLhctNavigation")]
    public virtual ICollection<Diemdanh> Diemdanhs { get; set; } = new List<Diemdanh>();

    [ForeignKey("IdLichhoc")]
    [InverseProperty("LichhocChitiets")]
    public virtual Lichhoc IdLichhocNavigation { get; set; } = null!;

    [ForeignKey("IdLoaikynang")]
    [InverseProperty("LichhocChitiets")]
    public virtual Loaikynang IdLoaikynangNavigation { get; set; } = null!;
}
