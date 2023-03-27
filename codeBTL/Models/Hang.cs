using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Hang
{
    public string MaHangSx { get; set; } = null!;

    public string TenHangSx { get; set; } = null!;

    public virtual ICollection<Dienthoai> Dienthoais { get; } = new List<Dienthoai>();
}
