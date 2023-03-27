using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Nhacungcap
{
    public string MaNcc { get; set; } = null!;

    public string TenNcc { get; set; } = null!;

    public string? DiaChiNcc { get; set; }

    public string? Sdtncc { get; set; }

    public virtual ICollection<Hoadonnhap> Hoadonnhaps { get; } = new List<Hoadonnhap>();
}
