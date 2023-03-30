using System;
using System.Collections.Generic;

namespace codeBTL.Models;

public partial class Userinfo
{
    public string UserId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Sdtuser { get; set; } = null!;

    public string DiaChiUser { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Role { get; set; }

    public virtual ICollection<Dondathang> Dondathangs { get; } = new List<Dondathang>();

    public virtual ICollection<Hoadonban> Hoadonbans { get; } = new List<Hoadonban>();
}
