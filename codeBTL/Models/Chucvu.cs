using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Chucvu
{
    public string MaCv { get; set; } = null!;

    public string TenCv { get; set; } = null!;

    public virtual ICollection<Nhanvien> Nhanviens { get; } = new List<Nhanvien>();
}
