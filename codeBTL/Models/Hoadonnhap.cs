using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Hoadonnhap
{
    public string SoHdn { get; set; } = null!;

    public string MaNcc { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public DateTime NgayNhap { get; set; }

    public decimal? TongTien { get; set; }

    public virtual Nhacungcap MaNccNavigation { get; set; } = null!;

    public virtual Nhanvien MaNvNavigation { get; set; } = null!;
}
