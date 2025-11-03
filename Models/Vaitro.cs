using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("VAITRO")]
public partial class Vaitro
{
    [Key]
    [Column("ID_VAITRO")]
    public int IdVaitro { get; set; }

    [Column("TENVAITRO")]
    [StringLength(50)]
    public string Tenvaitro { get; set; } = null!;

    [InverseProperty("IdSession1")]
    public virtual ICollection<Chatmessage> Chatmessages { get; set; } = new List<Chatmessage>();

    [InverseProperty("IdVaitroNavigation")]
    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
