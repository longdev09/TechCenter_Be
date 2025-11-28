using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Chatsession
{
    public int IdSession { get; set; }

    public int Loaisession { get; set; }

    public int? IdTaikhoan { get; set; }

    public DateTime? Thoidiembatdau { get; set; }

    public DateTime? Thoidiemketthuc { get; set; }

    public virtual ICollection<Chatmessage> Chatmessages { get; set; } = new List<Chatmessage>();

    public virtual Taikhoan? IdTaikhoanNavigation { get; set; }
}
