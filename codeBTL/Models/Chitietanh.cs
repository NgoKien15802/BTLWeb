using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Chitietanh
{
    public string MaDt { get; set; } = null!;

    public string TenFileAnh { get; set; } = null!;

    public virtual Dienthoai MaDtNavigation { get; set; } = null!;
}
