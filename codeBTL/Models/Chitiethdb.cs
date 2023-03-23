using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Chitiethdb
{
    public string MaDt { get; set; } = null!;

    public string SoHdb { get; set; } = null!;

    public decimal? KhuyenMai { get; set; }

    public int Slban { get; set; }

    public virtual Dienthoai MaDtNavigation { get; set; } = null!;

    public virtual Hoadonban SoHdbNavigation { get; set; } = null!;
}
