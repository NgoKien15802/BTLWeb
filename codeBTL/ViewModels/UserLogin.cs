using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace codeBTL.Models;

public partial class UserLogin
{
    [Required(ErrorMessage = "Mật khẩu không được để trống.")]
    [DisplayName("Mật khẩu")]
    public string Password { get; set; }

    [EmailAddress(ErrorMessage = "Định dạng email không hợp lệ.")]
    [DisplayName("Email")]
    public string? Email { get; set; }
}
