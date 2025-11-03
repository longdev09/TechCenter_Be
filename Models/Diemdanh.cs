using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[PrimaryKey("IdLhct", "IdHv")]
[Table("DIEMDANH")]
public partial class Diemdanh
{
    [Key]
    [Column("ID_LHCT")]
    public int IdLhct { get; set; }

    [Key]
    [Column("ID_HV")]
    public int IdHv { get; set; }

    [Column("TT_DIEMDANH")]
    public bool TtDiemdanh { get; set; }

    [ForeignKey("IdHv")]
    [InverseProperty("Diemdanhs")]
    public virtual Hocvien IdHvNavigation { get; set; } = null!;

    [ForeignKey("IdLhct")]
    [InverseProperty("Diemdanhs")]
    public virtual LichhocChitiet IdLhctNavigation { get; set; } = null!;
}
