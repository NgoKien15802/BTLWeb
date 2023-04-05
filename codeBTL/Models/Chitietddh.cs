using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace codeBTL.Models;

public partial class Chitietddh
{
    [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
    [DisplayName("Tên sản phẩm")]
    public string MaSp { get; set; } = null!;

    [Required(ErrorMessage = "Mã đơn hàng không được để trống.")]
    [DisplayName("Mã đơn hàng( DDH-xx )")]
    public string MaDh { get; set; } = null!;

    [DisplayName("Khuyến mãi")]
    public decimal? KhuyenMai { get; set; }

    [Required(ErrorMessage = "Số lượng đặt không được để trống.")]
    [DisplayName("Số lượng đặt")]
    public int? Sldat { get; set; }

    public virtual Dondathang? MaDhNavigation { get; set; } = null!;

    public virtual Sanpham? MaSpNavigation { get; set; } = null!;
}
