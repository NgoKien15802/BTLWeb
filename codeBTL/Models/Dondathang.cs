using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Dondathang
{
    public string MaDh { get; set; } = null!;

    public string MaNv { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime NgayDat { get; set; }

    public DateTime NgayGiao { get; set; } 

    public decimal TongTien { get; set; }

    public decimal TongThanhToan { get; set; }

    public virtual Nhanvien MaNvNavigation { get; set; } = null!;

    public virtual Userinfo User { get; set; } = null!;
}
