using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Chitiethdn
{
    public string MaSp { get; set; } = null!;

    public string SoHdn { get; set; } = null!;

    public decimal KhuyenMai { get; set; }

    public int Slnhap { get; set; }

    public virtual Sanpham MaSpNavigation { get; set; } = null!;

    public virtual Hoadonnhap SoHdnNavigation { get; set; } = null!;
}
