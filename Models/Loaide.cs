using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("LOAIDE")]
public partial class Loaide
{
    [Key]
    [Column("ID_LOAIDE")]
    public int IdLoaide { get; set; }

    [Column("TENLOAIDE")]
    [StringLength(20)]
    public string Tenloaide { get; set; } = null!;

    [InverseProperty("IdLoaideNavigation")]
    public virtual ICollection<Deluyen> Deluyens { get; set; } = new List<Deluyen>();
}
