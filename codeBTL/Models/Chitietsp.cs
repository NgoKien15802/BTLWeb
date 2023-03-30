using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Chitietsp
{
    public string MaChiTietSp { get; set; } = null!;

    public string MaSp { get; set; } = null!;

    public decimal DonGiaBan { get; set; }

    public decimal DonGiaNhap { get; set; }

    public string Ram { get; set; } = null!;

    public string Rom { get; set; } = null!;

    public string Cpu { get; set; } = null!;

    public string Dlpin { get; set; } = null!;

    public virtual Sanpham MaSpNavigation { get; set; } = null!;
}
