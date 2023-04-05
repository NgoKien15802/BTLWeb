using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace codeBTL.Models;

public partial class Dondathang
{
    [Required(ErrorMessage = "Mã đơn hàng không được để trống.")]
    [DisplayName("Mã đơn hàng( DDH-xx )")]
    public string MaDh { get; set; } = null!;

    [DisplayName("Tên nhân viên")]
    [Required(ErrorMessage = "Tên nhân viên không được để trống.")]
    public string? MaNv { get; set; } = null!;

    [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
    [DisplayName("Tên khách hàng")]
    public string? UserId { get; set; } = null!;

    [Required(ErrorMessage = "Ngày đặt không được để trống.")]
    [DisplayName("Ngày đặt")]
    public DateTime? NgayDat { get; set; }

    public DateTime? NgayGiao { get; set; }

    [Required(ErrorMessage = "Tổng tiền không được để trống.")]
    [DisplayName("Tổng tiền")]
    public decimal? TongTien { get; set; }

    public decimal? TongThanhToan { get; set; }

    public virtual Nhanvien? MaNvNavigation { get; set; } = null!;

    public virtual Userinfo? User { get; set; } = null!;
}
