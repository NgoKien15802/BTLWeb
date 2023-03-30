using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Loaisp
{
    public string MaLoai { get; set; } = null!;

    public string TenLoai { get; set; } = null!;

    public virtual ICollection<Sanpham> Sanphams { get; } = new List<Sanpham>();
}
