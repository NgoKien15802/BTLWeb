using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace codeBTL.Models;

public partial class Sanpham
{

    [Required(ErrorMessage = "Mã sản phẩm không được để trống.")]
    [DisplayName("Mã sản phẩm( DT-xx, PK-xx )")]
    public string MaSp { get; set; } = null!;

    [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
    [DisplayName("Tên sản phẩm")]
    public string TenSp { get; set; } = null!;


    [Required(ErrorMessage = "Loại sản phẩm không được để trống.")]
    [DisplayName("Loại sản phẩm")]
    public string MaLoai { get; set; } = null!;

    [Required(ErrorMessage = "Hãng sản xuất không được để trống.")]
    [DisplayName("Hãng sản xuất")]
    public string MaHangSx { get; set; } = null!;

    [FileExtensions(Extensions = "jpg,img", ErrorMessage = "đuôi file ảnh phải img, jpg")]
    [Required(ErrorMessage = "Ảnh đại diện không được để trống.")]
    [DisplayName("Ảnh đại diện")]
    public string AnhDaiDien { get; set; } = null!;

    [Required(ErrorMessage = "Số lượng không được để trống.")]
    [DisplayName("Số lượng")]
    public int SoLuong { get; set; }

    [DisplayName("Miêu tả")]
    public string? MieuTa { get; set; } = null!;

    [Required(ErrorMessage = "Màu sắc không được để trống.")]
    [DisplayName("Màu sắc")]
    public string MauSac { get; set; } = null!;

    [DisplayName("Trọng lượng")]
    public double? TrongLuong { get; set; }

    [DisplayName("Thời gian bảo hàng")]
    public int? ThoiGianBh { get; set; }

    public virtual ICollection<Chitietsp> Chitietsps { get; } = new List<Chitietsp>();

    public virtual Hang? MaHangSxNavigation { get; set; } = null!;

    public virtual Loaisp? MaLoaiNavigation { get; set; } = null!;

}
