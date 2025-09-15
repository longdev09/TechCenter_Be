using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Chatmessage
{
    public int IdMessage { get; set; }

    public int IdSession { get; set; }

    public int IdVaitro { get; set; }

    public int IdTaikhoan { get; set; }

    public string Noidungmessage { get; set; } = null!;

    public DateTime? Thoidiemgui { get; set; }

    public bool IsDagui { get; set; }

    public bool IsDaxem { get; set; }

    public virtual Vaitro IdSession1 { get; set; } = null!;

    public virtual Chatsession IdSessionNavigation { get; set; } = null!;

    public virtual Taikhoan IdTaikhoanNavigation { get; set; } = null!;
}
