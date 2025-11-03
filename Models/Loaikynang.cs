using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("LOAIKYNANG")]
public partial class Loaikynang
{
    [Key]
    [Column("ID_LOAIKYNANG")]
    public int IdLoaikynang { get; set; }

    [Column("TENLOAIKYNANG")]
    [StringLength(255)]
    public string Tenloaikynang { get; set; } = null!;

    [InverseProperty("IdLoaikynangNavigation")]
    public virtual ICollection<LichhocChitiet> LichhocChitiets { get; set; } = new List<LichhocChitiet>();
}
