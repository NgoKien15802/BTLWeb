using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Nhanvien
{
    public string MaNv { get; set; } = null!;

    public string MaCv { get; set; } = null!;

    public string TenNv { get; set; } = null!;

    public string Sdtnv { get; set; } = null!;

    public string? GioiTinh { get; set; }

    public string? DiaChiNv { get; set; }

    public DateTime? NgaySinh { get; set; }

    public virtual ICollection<Dondathang> Dondathangs { get; } = new List<Dondathang>();

    public virtual ICollection<Hoadonban> Hoadonbans { get; } = new List<Hoadonban>();

    public virtual ICollection<Hoadonnhap> Hoadonnhaps { get; } = new List<Hoadonnhap>();

    public virtual Chucvu MaCvNavigation { get; set; } = null!;
}
