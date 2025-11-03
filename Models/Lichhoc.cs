using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("LICHHOC")]
public partial class Lichhoc
{
    [Key]
    [Column("ID_LICHHOC")]
    public int IdLichhoc { get; set; }

    [Column("ID_LOPHOC")]
    public int IdLophoc { get; set; }

    [Column("THU")]
    public int Thu { get; set; }

    [Column("GIOBATDAU", TypeName = "datetime")]
    public DateTime Giobatdau { get; set; }

    [Column("GIOKETTHUC", TypeName = "datetime")]
    public DateTime Gioketthuc { get; set; }

    [ForeignKey("IdLophoc")]
    [InverseProperty("Lichhocs")]
    public virtual Lophoc IdLophocNavigation { get; set; } = null!;

    [InverseProperty("IdLichhocNavigation")]
    public virtual ICollection<LichhocChitiet> LichhocChitiets { get; set; } = new List<LichhocChitiet>();
}
