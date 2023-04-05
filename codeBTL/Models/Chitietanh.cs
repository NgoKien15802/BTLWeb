using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace codeBTL.Models;

public partial class Chitietanh
{
   
    public Guid MaCTA { get; set; } = Guid.Empty!;


    [DisplayName("Mã sản phẩm")]
    public string MaSp { get; set; } = null!;


    [DisplayName("Tên file ảnh")]
    [FileExtensions(Extensions = "jpg,img", ErrorMessage = "đuôi file ảnh phải img, jpg")]
    [Required(ErrorMessage = "Ảnh đại diện không được để trống.")]
    public string TenFileAnh { get; set; } = null!;

    public virtual Sanpham? MaSpNavigation { get; set; } = null!;
}

