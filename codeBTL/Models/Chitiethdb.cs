using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Chitiethdb
{
    public string MaSp { get; set; } = null!;

    public string SoHdb { get; set; } = null!;

    public decimal? KhuyenMai { get; set; }

    public int Slban { get; set; }

    public virtual Sanpham MaSpNavigation { get; set; } = null!;

    public virtual Hoadonban SoHdbNavigation { get; set; } = null!;
}
