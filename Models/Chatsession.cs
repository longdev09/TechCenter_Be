using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("CHATSESSION")]
public partial class Chatsession
{
    [Key]
    [Column("ID_SESSION")]
    public int IdSession { get; set; }

    [Column("LOAISESSION")]
    public int Loaisession { get; set; }

    [Column("ID_TAIKHOAN")]
    public int? IdTaikhoan { get; set; }

    [Column("THOIDIEMBATDAU", TypeName = "datetime")]
    public DateTime? Thoidiembatdau { get; set; }

    [Column("THOIDIEMKETTHUC", TypeName = "datetime")]
    public DateTime? Thoidiemketthuc { get; set; }

    [InverseProperty("IdSessionNavigation")]
    public virtual ICollection<Chatmessage> Chatmessages { get; set; } = new List<Chatmessage>();

    [ForeignKey("IdTaikhoan")]
    [InverseProperty("Chatsessions")]
    public virtual Taikhoan? IdTaikhoanNavigation { get; set; }
}
