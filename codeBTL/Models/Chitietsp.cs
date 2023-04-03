using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace codeBTL.Models;

public partial class Chitietsp
{

    [Required(ErrorMessage = "Mã chi tiết sản phẩm không được để trống.")]
    [DisplayName("Mã chi tiết sản phẩm( CTSP-xx )")]
    public string MaChiTietSp { get; set; } = null!;


    [Required(ErrorMessage = "Mã sản phẩm không được để trống.")]
    [DisplayName("Mã sản phẩm( DT-xx, PK-xx )")]
    public string MaSp { get; set; } = null!;


    [Required(ErrorMessage = "Đơn giá bán không được để trống.")]
    [DisplayName("Đơn giá bán")]
    public decimal DonGiaBan { get; set; }

    [DisplayName("Đơn giá nhập")]
    public decimal? DonGiaNhap { get; set; }


    public string? Ram { get; set; } = null!;

    public string? Rom { get; set; } = null!;

    public string? Cpu { get; set; } = null!;

    [DisplayName("Dung lượng pin")]
    public string? Dlpin { get; set; } = null!;

    public virtual Sanpham? MaSpNavigation { get; set; } = null!;
}
