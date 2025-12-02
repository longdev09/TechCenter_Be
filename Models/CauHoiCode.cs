using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CauHoiCode")]
public partial class CauHoiCode
{
    [Key]
    public int IdCauHoi { get; set; }

    public string? CodeMau { get; set; }

    [StringLength(50)]
    public string? NgonNgu { get; set; }

    [ForeignKey("IdCauHoi")]
    [InverseProperty("CauHoiCode")]
    public virtual Cauhoi IdCauHoiNavigation { get; set; } = null!;
}
