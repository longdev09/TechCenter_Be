using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("DAPAN")]
public partial class Dapan
{
    [Key]
    [Column("ID_DAPAN")]
    public int IdDapan { get; set; }

    [Column("ID_CAUHOI")]
    public int IdCauhoi { get; set; }

    [Column("ND_DAPAN")]
    [StringLength(255)]
    public string NdDapan { get; set; } = null!;

    [Column("IS_TRUE")]
    public bool IsTrue { get; set; }

    [ForeignKey("IdCauhoi")]
    [InverseProperty("Dapans")]
    public virtual Cauhoi IdCauhoiNavigation { get; set; } = null!;
}
