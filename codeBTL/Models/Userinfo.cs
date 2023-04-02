using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace codeBTL.Models;

public partial class Userinfo
{
    [Required(ErrorMessage = "Mã khách hàng không được để trống.")]
    [DisplayName("Mã khách hàng( KH-xx )")]
    public string UserId { get; set; }

    [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
    [DisplayName("Tên khách hàng")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được để trống.")]
    [DisplayName("Mật khẩu")]
    public string Password { get; set; }

    [DisplayName("Số điện thoại")]
    public string? Sdtuser { get; set; }

    [DisplayName("Địa chỉ")]
    public string? DiaChiUser { get; set; }

    [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
    [DisplayName("Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Vai trò không được để trống.")]
    [Range(0, 1, ErrorMessage = "Vai trò không hợp lệ ( 0:User, 1:Admin ).")]
    [DisplayName("Vai trò ( 0:User, 1:Admin )")]
    public int Role { get; set; }
    public virtual ICollection<Dondathang> Dondathangs { get; } = new List<Dondathang>();

    public virtual ICollection<Hoadonban> Hoadonbans { get; } = new List<Hoadonban>();
}
