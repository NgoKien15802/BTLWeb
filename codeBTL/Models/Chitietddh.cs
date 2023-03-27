﻿using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Chitietddh
{
    public string MaDt { get; set; } = null!;

    public string MaDh { get; set; } = null!;

    public decimal KhuyenMai { get; set; }

    public int Sldat { get; set; }

    public virtual Dondathang MaDhNavigation { get; set; } = null!;

    public virtual Dienthoai MaDtNavigation { get; set; } = null!;
}
