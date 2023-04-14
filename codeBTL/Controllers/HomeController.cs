using codeBTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

using codeBTL.Models.Authentication;
using codeBTL.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using codeBTL.ViewModels;
using System.Threading.Tasks.Dataflow;

namespace codeBTL.Controllers
{
    public class HomeController : Controller
    {
        DtddContext db = new DtddContext();
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       [Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            orderby sp.TenSp
                            select new Models.ViewModels.SanPhamViewModel
                            {
                                MaSp = sp.MaSp,
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = cts.DonGiaBan
                            }).OrderBy(x => x.TenSp);

            PagedList<SanPhamViewModel> pagedList = new PagedList<SanPhamViewModel>(products, pageNumber, pageSize);
            return View(pagedList);
        }

        public IActionResult SearchProducts(int? page, string? searchString)
        {

            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if(searchString != null)
            {
                var productsFilter = (from sp in db.Sanphams
                                      join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                                      where sp.TenSp.Contains(searchString) // Lọc sản phẩm theo tên
                                      orderby sp.TenSp
                                      select new Models.ViewModels.SanPhamViewModel
                                      {
                                          TenSp = sp.TenSp,
                                          AnhDaiDien = sp.AnhDaiDien,
                                          DonGiaBan = cts.DonGiaBan
                                      }).OrderBy(x => x.TenSp);

                PagedList<SanPhamViewModel> pagedListFilter = new PagedList<SanPhamViewModel>(productsFilter, pageNumber, pageSize);
                return PartialView("SearchProducts", pagedListFilter);
            }
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            orderby sp.TenSp
                            select new Models.ViewModels.SanPhamViewModel
                            {
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = cts.DonGiaBan
                            }).OrderBy(x => x.TenSp);

            PagedList<SanPhamViewModel> pagedList = new PagedList<SanPhamViewModel>(products, pageNumber, pageSize);
            return PartialView("SearchProducts", pagedList);
        }

        public ActionResult GetTop9Products()
        {
            var topProducts = (from ctdh in db.Chitietddhs
                               join sp in db.Sanphams on ctdh.MaSp equals sp.MaSp
                               join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                               orderby ctdh.Sldat descending
                               select new Models.ViewModels.SanPhamViewModel
                               {
                                   TenSp = sp.TenSp,
                                   AnhDaiDien = sp.AnhDaiDien,
                                   DonGiaBan = cts.DonGiaBan
                               }).Take(9).ToList();

            return PartialView("GetTop9Products", topProducts);
        }

        public IActionResult SmartPhone(int? page, string searchString = null, int pageSize = 9)
        {
            ViewBag.psz = pageSize;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if(searchString != null)
            {
                var productsSearch = (from sp in db.Sanphams
                                join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                                where sp.TenSp.Contains(searchString) && sp.MaLoai == "LOAI01"
                                orderby sp.TenSp
                                select new Models.ViewModels.SanPhamViewModel
                                {
                                    MaSp = sp.MaSp,
                                    TenSp = sp.TenSp,
                                    AnhDaiDien = sp.AnhDaiDien,
                                    DonGiaBan = cts.DonGiaBan
                                }).OrderBy(x => x.TenSp);
                ViewBag.Search = searchString;
                PagedList<SanPhamViewModel> pagedListSearch = new PagedList<SanPhamViewModel>(productsSearch, pageNumber, pageSize);
                return View(pagedListSearch);
            }
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            where sp.MaLoai == "LOAI01"
                            orderby sp.TenSp
                            select new Models.ViewModels.SanPhamViewModel
                            {
                                MaSp = sp.MaSp,
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = cts.DonGiaBan
                            }).OrderBy(x => x.TenSp);

            PagedList<SanPhamViewModel> pagedList = new PagedList<SanPhamViewModel>(products, pageNumber, pageSize);
            return View(pagedList);
        }

        public IActionResult SmartphoneByBrand(string maHang, int? page, string searchString = null, int pageSize = 9)
        {
            ViewBag.psz = pageSize;
            ViewBag.MaHang = maHang;
            ViewBag.TenHang = db.Hangs.Where(x=>x.MaHangSx == maHang).Select(x=>x.TenHangSx).FirstOrDefault();
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if (searchString != null)
            {
                var productsSearch = (from sp in db.Sanphams
                                      join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                                      where sp.TenSp.Contains(searchString) && sp.MaLoai == "LOAI01"
                                      orderby sp.TenSp
                                      select new Models.ViewModels.SanPhamViewModel
                                      {
                                          MaSp = sp.MaSp,
                                          TenSp = sp.TenSp,
                                          AnhDaiDien = sp.AnhDaiDien,
                                          DonGiaBan = cts.DonGiaBan
                                      }).OrderBy(x => x.TenSp);
                ViewBag.Search = searchString;
                PagedList<SanPhamViewModel> pagedListSearch = new PagedList<SanPhamViewModel>(productsSearch, pageNumber, pageSize);
                return View(pagedListSearch);
            }
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            where sp.MaLoai == "LOAI01" && sp.MaHangSx == maHang
                            orderby sp.TenSp
                            select new Models.ViewModels.SanPhamViewModel
                            {
                                MaSp = sp.MaSp,
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = cts.DonGiaBan
                            }).OrderBy(x => x.TenSp);

            PagedList<SanPhamViewModel> pagedList = new PagedList<SanPhamViewModel>(products, pageNumber, pageSize);
            return View(pagedList);
        }
        public IActionResult PhoneAccessories(int? page, string searchString = null, int pageSize = 9)
        {
            ViewBag.psz = pageSize;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if (searchString != null)
            {
                var productsSearch = (from sp in db.Sanphams
                                      join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                                      where sp.TenSp.Contains(searchString) && sp.MaLoai == "LOAI02"
                                      orderby sp.TenSp
                                      select new Models.ViewModels.SanPhamViewModel
                                      {
                                          MaSp = sp.MaSp,
                                          TenSp = sp.TenSp,
                                          AnhDaiDien = sp.AnhDaiDien,
                                          DonGiaBan = cts.DonGiaBan
                                      }).OrderBy(x => x.TenSp);
                ViewBag.Search = searchString;
                PagedList<SanPhamViewModel> pagedListSearch = new PagedList<SanPhamViewModel>(productsSearch, pageNumber, pageSize);
                return View(pagedListSearch);
            }
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            where sp.MaLoai == "LOAI02"
                            orderby sp.TenSp
                            select new Models.ViewModels.SanPhamViewModel
                            {
                                MaSp = sp.MaSp,
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = cts.DonGiaBan
                            }).OrderBy(x => x.TenSp);

            PagedList<SanPhamViewModel> pagedList = new PagedList<SanPhamViewModel>(products, pageNumber, pageSize);
            return View(pagedList);
        }

        public JsonResult GetSuggestions(string keyword)
        {
            var products = (from sp in db.Sanphams
                            join cts in db.Chitietsps on sp.MaSp equals cts.MaSp
                            where sp.TenSp.Contains(keyword)
                            orderby sp.TenSp
                            select new Models.ViewModels.SanPhamViewModel
                            {
                                MaSp = sp.MaSp,
                                TenSp = sp.TenSp,
                                AnhDaiDien = sp.AnhDaiDien,
                                DonGiaBan = cts.DonGiaBan
                            }).Take(5).OrderBy(x => x.TenSp);
            return Json(products);
        }
        public IActionResult News()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View();
        }

        public IActionResult SmartphoneDetails(string? MaSp)
        {

            var sanPham = (from sp in db.Sanphams
                           join ctsp in db.Chitietsps on sp.MaSp equals ctsp.MaSp
                           where sp.MaSp == MaSp
                           select new SanPhamDetailsViewModel
                           {
                               MaSp = MaSp,
                               TenSP = sp.TenSp,
                               DonGiaBan=ctsp.DonGiaBan,
                               MaHangSx=sp.MaHangSx,
                               AnhDaiDien=sp.AnhDaiDien,
                               SoLuong=sp.SoLuong,
                               MieuTa=sp.MieuTa,
                               MauSac=sp.MauSac,
                               TrongLuong=sp.TrongLuong,
                               ThoiGianBh=sp.ThoiGianBh,
                               Ram=ctsp.Ram,
                               Rom = ctsp.Rom,
                               Cpu = ctsp.Cpu,
                               Dlpin = ctsp.Dlpin,
                           }).FirstOrDefault();
            var lstAnhSp = db.Chitietanhs.Where(x => x.MaSp == MaSp).ToList();

            var cungHangSanPham = (from sp in db.Sanphams
                                   join ctsp in db.Chitietsps on sp.MaSp equals ctsp.MaSp
                                   where sp.MaHangSx == sanPham.MaHangSx
                                   select new SanPhamDetailsViewModel
                                   {
                                       MaSp = sp.MaSp,
                                       TenSP = sp.TenSp,
                                       DonGiaBan = ctsp.DonGiaBan,
                                       MaHangSx = sp.MaHangSx,
                                       AnhDaiDien = sp.AnhDaiDien,
                                   }).ToList();
           
            ViewBag.lstAnhSp = lstAnhSp;
            ViewBag.spCungHang = cungHangSanPham;
            if (sanPham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanPham);
        }

        public IActionResult Order(string? maDh)
        {
            var orders = from ddh in db.Dondathangs
                         join user in db.Userinfos on ddh.UserId equals user.UserId
                         join ctdh in db.Chitietddhs on ddh.MaDh equals ctdh.MaDh
                         join sp in db.Sanphams on ctdh.MaSp equals sp.MaSp
                         join ctsp in db.Chitietsps on sp.MaSp equals ctsp.MaSp
                         where ddh.MaDh == maDh
                         select new OrderDetailsViewModel
                         {
                             MaDh = ddh.MaDh,
                             TenSp = sp.TenSp,
                             Sldat = ctdh.Sldat,
                             Username = user.Username,
                             MaSp = sp.MaSp,
                             NgayDat = ddh.NgayDat,
                             DonGiaBan = ctsp.DonGiaBan,
                             DiaChiUser=user.DiaChiUser,
                             TongTien = ddh.TongTien
                         };

            if (orders != null)
            {
                var orderList = orders.FirstOrDefault();
                return View(orderList);
            }
            else
            {
                return View();
            }
        }

        public IActionResult CreateOrder(string? MaSp)
        {

            // Tạo đơn đặt hàng mới
            var donDatHang = new Dondathang();

            // Tạo mã đơn đặt hàng tự động
            int nextMaDh = db.Dondathangs.Count() + 1;
            donDatHang.MaDh = "DDH" + nextMaDh.ToString();
            string userId = HttpContext.Session.GetString("UserId");
            if (userId != null)
            {
                donDatHang.UserId = userId;
                // Lấy UserId của người đăng nhập hiện tại

                // Cập nhật thông tin cho đơn đặt hàng mới
                donDatHang.NgayDat = DateTime.Now;
                donDatHang.MaNv = "NV01";
                var ctsp = db.Chitietsps.AsNoTracking().Where(x => x.MaSp == MaSp).FirstOrDefault();
                donDatHang.TongTien = 0;
                // Thêm đơn đặt hàng vào cơ sở dữ liệu
                db.Dondathangs.Add(donDatHang);
                db.SaveChanges();

                // Tạo chi tiết đơn đặt hàng mới cho sản phẩm được chọn

                var chiTietDDH = new Chitietddh();
                chiTietDDH.MaDh = donDatHang.MaDh;
                chiTietDDH.MaSp = MaSp;
                chiTietDDH.Sldat = 1;

                db.Chitietddhs.Add(chiTietDDH);
                db.SaveChanges();

                // Chuyển hướng sang trang ChiTietDDH với mã đơn đặt hàng
                return RedirectToAction("Order", new { maDh = donDatHang.MaDh });
            }
            else
            {
                return RedirectToAction("Login", "Access");
            }
        }

      

        public IActionResult HistoryOrder()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}