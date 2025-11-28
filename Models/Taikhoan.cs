using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Taikhoan
{
    public int IdTaikhoan { get; set; }

    public int IdVaitro { get; set; }

    public string Tendangnhap { get; set; } = null!;

    public string Matkhauhash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly? Ngaytao { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Chatmessage> Chatmessages { get; set; } = new List<Chatmessage>();

    public virtual ICollection<Chatsession> Chatsessions { get; set; } = new List<Chatsession>();

    public virtual Giaovien? Giaovien { get; set; }

    public virtual Hocvien? Hocvien { get; set; }

    public virtual Vaitro IdVaitroNavigation { get; set; } = null!;
}
