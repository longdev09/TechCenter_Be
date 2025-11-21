using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TechCenter.Models;

[Table("TAIKHOAN")]
[Index("Email", Name = "UQ__TAIKHOAN__161CF724C49F7DE0", IsUnique = true)]
[Index("Tendangnhap", Name = "UQ__TAIKHOAN__6C836FE53C594D7B", IsUnique = true)]
public partial class Taikhoan
{
    [Key]
    [Column("ID_TAIKHOAN")]
    public int IdTaikhoan { get; set; }

    [Column("ID_VAITRO")]
    public int IdVaitro { get; set; }

    [Column("TENDANGNHAP")]
    [StringLength(50)]
    [Unicode(false)]
    public string Tendangnhap { get; set; } = null!;

    [Column("MATKHAUHASH")]
    [StringLength(255)]
    [Unicode(false)]
    public string Matkhauhash { get; set; } = null!;

    [Column("EMAIL")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("NGAYTAO")]
    public DateOnly? Ngaytao { get; set; }

    [Column("IS_ACTIVE")]
    public bool IsActive { get; set; }

    [InverseProperty("IdTaikhoanNavigation")]
    public virtual ICollection<Chatmessage> Chatmessages { get; set; } = new List<Chatmessage>();

    [InverseProperty("IdTaikhoanNavigation")]
    public virtual ICollection<Chatsession> Chatsessions { get; set; } = new List<Chatsession>();

    [InverseProperty("IdTkNavigation")]
    public virtual ICollection<Deluyen> Deluyens { get; set; } = new List<Deluyen>();

    [InverseProperty("IdTaikhoanNavigation")]
    public virtual Giaovien? Giaovien { get; set; }

    [InverseProperty("IdTaikhoanNavigation")]
    public virtual Hocvien? Hocvien { get; set; }

    [ForeignKey("IdVaitro")]
    [InverseProperty("Taikhoans")]
    public virtual Vaitro IdVaitroNavigation { get; set; } = null!;
}
