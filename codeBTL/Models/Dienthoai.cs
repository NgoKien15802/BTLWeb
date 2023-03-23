using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Dienthoai
{
    public string MaDt { get; set; } = null!;

    public string MaHangSx { get; set; } = null!;

    public string TenDt { get; set; } = null!;

    public decimal DonGiaBan { get; set; }

    public int SoLuong { get; set; }

    public decimal DonGiaNhap { get; set; }

    public string MieuTa { get; set; } = null!;

    public string Ram { get; set; } = null!;

    public string Rom { get; set; } = null!;

    public string MauSac { get; set; } = null!;

    public string Cpu { get; set; } = null!;

    public int ThoiGianBh { get; set; }

    public string Dlpin { get; set; } = null!;

    public double TrongLuong { get; set; }

    public string TenFileAnh { get; set; } = null!;

    public string DanhGia { get; set; } = null!;

    public virtual Hang MaHangSxNavigation { get; set; } = null!;
}
