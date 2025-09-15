using System;
using System.Collections.Generic;

namespace TechCenter.Models;

public partial class Loaide
{
    public int IdLoaide { get; set; }

    public string Tenloaide { get; set; } = null!;

    public virtual ICollection<Deluyen> Deluyens { get; set; } = new List<Deluyen>();
}
