using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechCenter.Models;

public partial class Vaitro
{
    public int IdVaitro { get; set; }

    public string Tenvaitro { get; set; } = null!;

    public virtual ICollection<Chatmessage> Chatmessages { get; set; } = new List<Chatmessage>();
    public virtual ICollection<Taikhoan> Taikhoans { get; set; } = new List<Taikhoan>();
}
