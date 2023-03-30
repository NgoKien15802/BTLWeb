using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Sanpham
{
    public string MaSp { get; set; } = null!;

    public string TenSp { get; set; } = null!;

    public string MaLoai { get; set; } = null!;

    public string MaHangSx { get; set; } = null!;

    public string AnhDaiDien { get; set; } = null!;

    public int SoLuong { get; set; }

    public string MieuTa { get; set; } = null!;

    public string MauSac { get; set; } = null!;

    public double TrongLuong { get; set; }

    public int ThoiGianBh { get; set; }

    public virtual ICollection<Chitietsp> Chitietsps { get; } = new List<Chitietsp>();

    public virtual Hang MaHangSxNavigation { get; set; } = null!;

    public virtual Loaisp MaLoaiNavigation { get; set; } = null!;
}
