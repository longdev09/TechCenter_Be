using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CHATMESSAGE")]
public partial class Chatmessage
{
    [Key]
    [Column("ID_MESSAGE")]
    public int IdMessage { get; set; }

    [Column("ID_SESSION")]
    public int IdSession { get; set; }

    [Column("ID_VAITRO")]
    public int IdVaitro { get; set; }

    [Column("ID_TAIKHOAN")]
    public int IdTaikhoan { get; set; }

    [Column("NOIDUNGMESSAGE")]
    [StringLength(255)]
    public string Noidungmessage { get; set; } = null!;

    [Column("THOIDIEMGUI", TypeName = "datetime")]
    public DateTime? Thoidiemgui { get; set; }

    [Column("IS_DAGUI")]
    public bool IsDagui { get; set; }

    [Column("IS_DAXEM")]
    public bool IsDaxem { get; set; }

    [ForeignKey("IdSession")]
    [InverseProperty("Chatmessages")]
    public virtual Vaitro IdSession1 { get; set; } = null!;

    [ForeignKey("IdSession")]
    [InverseProperty("Chatmessages")]
    public virtual Chatsession IdSessionNavigation { get; set; } = null!;

    [ForeignKey("IdTaikhoan")]
    [InverseProperty("Chatmessages")]
    public virtual Taikhoan IdTaikhoanNavigation { get; set; } = null!;
}
